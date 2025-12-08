using System.Net;
using System.Text;
using System.Text.Json;

namespace sistemaFacturacion.Services;

public sealed class UsuariosApiClient : IUsuariosApiClient
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _http;

    public UsuariosApiClient(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<UsuarioDto>> GetAllAsync(CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, "api/Usuarios");
        using var res = await _http.SendAsync(req, ct);
        await EnsureSuccess(res);
        return Deserialize<IReadOnlyList<UsuarioDto>>(await res.Content.ReadAsStringAsync(ct));
    }

    public async Task<UsuarioDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, $"api/Usuarios/{id}");
        using var res = await _http.SendAsync(req, ct);
        if (res.StatusCode == HttpStatusCode.NotFound) return null;
        await EnsureSuccess(res);
        return Deserialize<UsuarioDto>(await res.Content.ReadAsStringAsync(ct));
    }

    public async Task<UsuarioDto> CreateEmpleadoAsync(CreateEmpleadoRequest request, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Post, "api/Usuarios/como-empleado")
        {
            Content = ToJson(request)
        };
        using var res = await _http.SendAsync(req, ct);
        await EnsureSuccess(res, HttpStatusCode.Created);
        return Deserialize<UsuarioDto>(await res.Content.ReadAsStringAsync(ct));
    }

    public async Task<UsuarioDto> UpdateDatosAsync(UpdateUsuarioRequest request, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Put, "api/Usuarios/actualizar-datos")
        {
            Content = ToJson(request)
        };
        using var res = await _http.SendAsync(req, ct);
        await EnsureSuccess(res);
        return Deserialize<UsuarioDto>(await res.Content.ReadAsStringAsync(ct));
    }

    public async Task<UsuarioDto> UpdateDatosByIdAsync(int id, UpdateUsuarioRequest request, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Put, $"api/Usuarios/actualizar-datos/{id}")
        {
            Content = ToJson(request)
        };
        using var res = await _http.SendAsync(req, ct);
        await EnsureSuccess(res);
        return Deserialize<UsuarioDto>(await res.Content.ReadAsStringAsync(ct));
    }

    public async Task<bool> CambiarContrasenaAsync(ChangePasswordRequest request, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Post, "api/Usuarios/cambiar-contrasena")
        {
            Content = ToJson(request)
        };
        using var res = await _http.SendAsync(req, ct);
        await EnsureSuccess(res);
        return true;
    }

    private static StringContent ToJson<T>(T payload) =>
        new(JsonSerializer.Serialize(payload, JsonOptions), Encoding.UTF8, "application/json");

    private static T Deserialize<T>(string content) =>
        JsonSerializer.Deserialize<T>(content, JsonOptions)
        ?? throw new JsonException("No se pudo deserializar la respuesta.");

    private static async Task EnsureSuccess(HttpResponseMessage res, HttpStatusCode? expected = null)
    {
        if (expected.HasValue && res.StatusCode != expected.Value)
        {
            var b = await res.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Estado inesperado: {(int)res.StatusCode} ({res.StatusCode}). {b}");
        }
        if (!res.IsSuccessStatusCode)
        {
            var b = await res.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error HTTP {(int)res.StatusCode} ({res.StatusCode}). {b}");
        }
    }
}
