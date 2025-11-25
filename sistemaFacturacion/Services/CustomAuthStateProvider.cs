using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly SessionService _sessionService;
    private readonly HttpClient _httpClient;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthStateProvider(SessionService sessionService, HttpClient httpClient)
    {
        _sessionService = sessionService;
        _httpClient = httpClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _sessionService.GetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(_anonymous);
            }

            if (IsTokenExpired(token))
            {
                await _sessionService.ClearAllSession();
                _httpClient.DefaultRequestHeaders.Authorization = null;
                return new AuthenticationState(_anonymous);
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(
                new ClaimsIdentity(ParseClaimsFromJwt(token), "jwtAuthType")));
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public void NotifyUserAuthentication(string token)
    {
        if (IsTokenExpired(token))
        {
            var authStateExpired = Task.FromResult(new AuthenticationState(_anonymous));
            _httpClient.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(authStateExpired);
            return;
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var authenticatedUser = new ClaimsPrincipal(
            new ClaimsIdentity(ParseClaimsFromJwt(token), "jwtAuthType"));

        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(new AuthenticationState(_anonymous));
        _httpClient.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(authState);
    }

    private bool IsTokenExpired(string jwt)
    {
        try
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs == null || !keyValuePairs.TryGetValue("exp", out var expValue) || expValue == null)
            {
                return false;
            }

            if (!long.TryParse(expValue.ToString(), out var expSeconds))
            {
                return false;
            }

            var expDateUtc = DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime;
            return expDateUtc <= DateTime.UtcNow;
        }
        catch
        {
            return true;
        }
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];

        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null)
        {
            if (keyValuePairs.TryGetValue("role_name", out object? roleName) && roleName != null)
            {
                var roleNameValue = roleName.ToString();
                if (!string.IsNullOrEmpty(roleNameValue))
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleNameValue));
                }
            }

            if (keyValuePairs.TryGetValue("sub", out object? sub) && sub != null)
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, sub.ToString() ?? ""));
            }

            if (keyValuePairs.TryGetValue("name", out object? name) && name != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, name.ToString() ?? ""));
            }

            if (keyValuePairs.TryGetValue("email", out object? email) && email != null)
            {
                claims.Add(new Claim(ClaimTypes.Email, email.ToString() ?? ""));
            }

            if (keyValuePairs.TryGetValue("idUsu", out object? idUsu) && idUsu != null)
            {
                claims.Add(new Claim("idUsu", idUsu.ToString() ?? ""));
            }
            if (keyValuePairs.TryGetValue("idEmp", out object? idEmp) && idEmp != null)
            {
                claims.Add(new Claim("idEmp", idEmp.ToString() ?? ""));
            }

            if (keyValuePairs.TryGetValue("nomEmp", out object? nomEmp) && nomEmp != null)
            {
                claims.Add(new Claim("nomEmp", nomEmp.ToString() ?? ""));
            }

        }

        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
