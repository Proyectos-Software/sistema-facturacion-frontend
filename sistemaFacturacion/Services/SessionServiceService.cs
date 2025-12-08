using Blazored.LocalStorage;
using System.Threading.Tasks;
using System.Text.Json;

public class SessionService
{
    private readonly ILocalStorageService _localStorage;
    private const string TokenKey = "authToken";

    public SessionService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task SetAppStateToSession(AppState state)
    {
        await _localStorage.SetItemAsync("AppState", state);
    }

    public async Task DeleteAppStateFromSession()
    {
        await _localStorage.RemoveItemAsync("AppState");
    }

    public async Task<AppState> GetAppStateFromSession()
    {
        try
        {
            var appState = await _localStorage.GetItemAsync<AppState>("AppState");
            return appState ?? new AppState();
        }
        catch
        {
            return new AppState();
        }
    }

    public async Task SetInitalAppStateToSession(AppState state)
    {
        await _localStorage.SetItemAsync("InitalAppState", state);
    }

    public async Task<AppState?> GetInitalAppStateFromSession()
    {
        try
        {
            var appState = await _localStorage.GetItemAsync<AppState>("InitalAppState");
            return appState;
        }
        catch
        {
            return null;
        }
    }

    public async Task SaveTokenAsync(string token)
    {
        await _localStorage.SetItemAsync("auth_token", token?.Trim().Trim('"'));
    }

    public async Task<string> GetTokenAsync()
    {
        var raw = await _localStorage.GetItemAsync<string>("auth_token");
        return string.IsNullOrWhiteSpace(raw) ? null : raw.Trim().Trim('"');
    }

    public async Task RemoveTokenAsync()
    {
        await _localStorage.RemoveItemAsync(TokenKey);
    }

    public async Task ClearAllSession()
    {
        await _localStorage.ClearAsync();
    }
}
