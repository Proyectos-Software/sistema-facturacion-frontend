using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly SessionService _sessionService;

    public sealed record RecoverPasswordResult(bool Success, bool NotFound, string? Message);
    public sealed record ValidateResetTokenResult(bool Success, string? ResetSessionId, string? Message);
    public sealed record ResetPasswordResult(bool Success, bool InvalidOrExpired, string? Message);
    private sealed record ResetPasswordResponse(bool Success, string? ErrorMessage);

    public AuthService(
        HttpClient httpClient,
        AuthenticationStateProvider authenticationStateProvider,
        SessionService sessionService)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _sessionService = sessionService;
    }

    public async Task<bool> LoginAsync(LoginRequest loginRequest)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return false;

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (loginResponse == null || string.IsNullOrEmpty(loginResponse.Token))
                return false;

            await _sessionService.SaveTokenAsync(loginResponse.Token);
            ((CustomAuthStateProvider)_authenticationStateProvider).NotifyUserAuthentication(loginResponse.Token);

            return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<RecoverPasswordResult> RecoverPasswordAsync(string email, CancellationToken ct = default)
    {
        using var resp = await _httpClient.PostAsJsonAsync("api/auth/recover-password", new { email }, ct);

        if (resp.IsSuccessStatusCode)
            return new RecoverPasswordResult(true, false, null);

        if (resp.StatusCode == HttpStatusCode.NotFound)
        {
            var txt = await resp.Content.ReadAsStringAsync(ct);
            var msg = string.IsNullOrWhiteSpace(txt) ? "Usuario no encontrado." : txt;
            return new RecoverPasswordResult(false, true, msg);
        }

        var body = await resp.Content.ReadAsStringAsync(ct);
        var fallback = string.IsNullOrWhiteSpace(body) ? "No se pudo procesar la solicitud." : body;
        return new RecoverPasswordResult(false, false, fallback);
    }

    public async Task<bool> SendPasswordResetEmailAsync(string email, CancellationToken ct = default)
    {
        var r = await RecoverPasswordAsync(email, ct);
        return r.Success;
    }

    public async Task LogoutAsync()
    {
        await _sessionService.ClearAllSession();
        ((CustomAuthStateProvider)_authenticationStateProvider).NotifyUserLogout();
    }

    public async Task<ResetPasswordResult> ResetPasswordAsync(
        string token,
        string newPassword,
        CancellationToken ct = default)
    {
        var payload = new { token, newPassword };

        using var resp = await _httpClient.PostAsJsonAsync("api/auth/reset-password", payload, ct);

        if (resp.IsSuccessStatusCode)
            return new ResetPasswordResult(true, false, null);

        if (resp.StatusCode == HttpStatusCode.BadRequest || resp.StatusCode == HttpStatusCode.NotFound)
        {
            var txt = await resp.Content.ReadAsStringAsync(ct);
            var msg = string.IsNullOrWhiteSpace(txt)
                ? "El enlace de recuperación es inválido o ha caducado."
                : txt;

            return new ResetPasswordResult(false, true, msg);
        }

        var body = await resp.Content.ReadAsStringAsync(ct);
        var fallback = string.IsNullOrWhiteSpace(body)
            ? "No se pudo restablecer la contraseña."
            : body;

        return new ResetPasswordResult(false, false, fallback);
    }

    public async Task<int?> GetCurrentUserIdAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated != true)
            return null;

        var claim = user.FindFirst("idUsu") ?? user.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null)
            return null;

        if (int.TryParse(claim.Value, out var id))
            return id;

        return null;
    }


    public async Task<int?> GetCurrentEmpresaIdAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (!user.Identity?.IsAuthenticated ?? false)
            return null;

        var claim = user.FindFirst("idEmp");
        if (claim == null)
            return null;

        if (int.TryParse(claim.Value, out var idEmpresa))
            return idEmpresa;

        return null;
    }

}
