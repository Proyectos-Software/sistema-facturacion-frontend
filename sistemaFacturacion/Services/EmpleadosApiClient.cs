using sistemaFacturacion.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace sistemaFacturacion.Services;

public sealed class EmpleadosApiClient : IEmpleadosApiClient
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _http;

    public EmpleadosApiClient(HttpClient http) => _http = http;

    public async Task<IReadOnlyList<EmpleadoDto>> GetAllAsync(CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, "api/Empleados");
        using var res = await _http.SendAsync(req, ct);
        await EnsureSuccess(res);
        var payload = await res.Content.ReadAsStringAsync(ct);
        return Deserialize<IReadOnlyList<EmpleadoDto>>(payload);
    }

    public async Task<EmpleadoDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, $"api/Empleados/{id}");
        using var res = await _http.SendAsync(req, ct);
        if (res.StatusCode == HttpStatusCode.NotFound) return null;
        await EnsureSuccess(res);
        var payload = await res.Content.ReadAsStringAsync(ct);
        return Deserialize<EmpleadoDto>(payload);
    }

    public async Task<bool> UpdateAsync(int id, UpdateEmpleadoRequest request, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Put, $"api/Empleados/{id}")
        {
            Content = ToJson(request)
        };
        using var res = await _http.SendAsync(req, ct);
        if (res.StatusCode == HttpStatusCode.NoContent) return true;
        await EnsureSuccess(res);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
    {
        using var req = new HttpRequestMessage(HttpMethod.Delete, $"api/Empleados/{id}");
        using var res = await _http.SendAsync(req, ct);

        if (res.StatusCode == HttpStatusCode.NoContent) return true;
        if (res.StatusCode == HttpStatusCode.NotFound) return false;

        await EnsureSuccess(res);
        return true;
    }

    private static StringContent ToJson<T>(T payload) =>
        new(JsonSerializer.Serialize(payload, JsonOptions), Encoding.UTF8, "application/json");

    private static T Deserialize<T>(string content) =>
        JsonSerializer.Deserialize<T>(content, JsonOptions)
        ?? throw new JsonException("No se pudo deserializar la respuesta.");

    private static async Task EnsureSuccess(HttpResponseMessage res)
    {
        if (!res.IsSuccessStatusCode)
        {
            var b = await res.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error HTTP {(int)res.StatusCode} ({res.StatusCode}). {b}");
        }
    }
}

