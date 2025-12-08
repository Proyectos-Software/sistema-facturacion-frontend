using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using sistemaFacturacion.Models;

namespace sistemaFacturacion.Services;

public class LotesApiClient : ILotesApiClient
{
    private readonly HttpClient _http;
    private readonly SessionService _sessionService;

    private const string Resource = "api/Lotes";

    private static readonly JsonSerializerOptions _json = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public LotesApiClient(HttpClient http, SessionService sessionService)
    {
        _http = http;
        _sessionService = sessionService;
    }

    private async Task EnsureAuthAsync()
    {
        var token = await _sessionService.GetTokenAsync();

        Console.WriteLine("=======================================");
        Console.WriteLine(" 🔍 DEBUG TOKEN PARA LOTES API CLIENT ");
        Console.WriteLine("=======================================");
        Console.WriteLine($"Token obtenido desde SessionService:");
        Console.WriteLine(string.IsNullOrWhiteSpace(token)
            ? "⚠️ TOKEN ES NULO o VACÍO"
            : $"🟢 TOKEN: {token}");

        Console.WriteLine("---------------------------------------");
        Console.WriteLine("Headers ANTES de modificar:");
        foreach (var h in _http.DefaultRequestHeaders)
            Console.WriteLine($"   {h.Key}: {string.Join(",", h.Value)}");
        Console.WriteLine("---------------------------------------");

        if (string.IsNullOrWhiteSpace(token))
            throw new HttpRequestException(
                "No hay token en SessionService. Inicia sesión nuevamente.",
                null,
                HttpStatusCode.Unauthorized
            );

        _http.DefaultRequestHeaders.Remove("Authorization");
        _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim());

        Console.WriteLine("Headers DESPUÉS de modificar:");
        foreach (var h in _http.DefaultRequestHeaders)
            Console.WriteLine($"   {h.Key}: {string.Join(",", h.Value)}");
        Console.WriteLine("=======================================");
    }

    private static string BuildErrorMessage(string accion, HttpStatusCode statusCode, string? body)
    {
        if (!string.IsNullOrWhiteSpace(body) &&
            body.Contains("Usuario no autenticado", StringComparison.OrdinalIgnoreCase))
        {
            return "Usuario no autenticado. Por favor inicia sesión nuevamente.";
        }

        if (string.IsNullOrWhiteSpace(body))
            return $"Error al {accion} ({(int)statusCode}).";

        return $"Error al {accion} ({(int)statusCode}): {body}";
    }

    public async Task<LoteDto?> CreateAsync(LoteCreateRequest dto, CancellationToken ct = default)
    {
        await EnsureAuthAsync();

        Console.WriteLine("📤 Enviando POST /api/Lotes ...");
        Console.WriteLine(JsonSerializer.Serialize(dto, _json));

        using var content = new StringContent(JsonSerializer.Serialize(dto, _json), Encoding.UTF8, "application/json");
        using var resp = await _http.PostAsync(Resource, content, ct);

        Console.WriteLine($"📥 StatusCode: {resp.StatusCode}");

        var bodyResp = await resp.Content.ReadAsStringAsync(ct);
        Console.WriteLine($"📥 Body: {bodyResp}");

        if (!resp.IsSuccessStatusCode)
            throw new HttpRequestException(
                BuildErrorMessage("crear lote", resp.StatusCode, bodyResp),
                null,
                resp.StatusCode);

        return JsonSerializer.Deserialize<LoteDto>(bodyResp, _json);
    }

    public async Task<LoteDto> UpdateAsync(int id, LoteUpdateRequest dto, CancellationToken ct = default)
    {
        await EnsureAuthAsync();

        Console.WriteLine($"📤 PUT /api/Lotes/{id}");
        Console.WriteLine(JsonSerializer.Serialize(dto, _json));

        var json = JsonSerializer.Serialize(dto, _json);
        using var resp = await _http.PutAsync($"{Resource}/{id}",
            new StringContent(json, Encoding.UTF8, "application/json"), ct);

        Console.WriteLine($"📥 StatusCode: {resp.StatusCode}");

        var bodyResp = await resp.Content.ReadAsStringAsync(ct);

        if (resp.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException($"Lote {id} no encontrado.", null, resp.StatusCode);

        if (!resp.IsSuccessStatusCode)
            throw new HttpRequestException(
                BuildErrorMessage("actualizar lote", resp.StatusCode, bodyResp),
                null,
                resp.StatusCode);

        return JsonSerializer.Deserialize<LoteDto>(bodyResp, _json)!;
    }

    public async Task<List<LoteDto>> GetAllAsync(CancellationToken ct = default)
    {
        await EnsureAuthAsync();

        Console.WriteLine("📤 GET /api/Lotes");

        using var resp = await _http.GetAsync(Resource, ct);

        Console.WriteLine($"📥 StatusCode: {resp.StatusCode}");

        var body = await resp.Content.ReadAsStringAsync(ct);
        if (!resp.IsSuccessStatusCode)
        {
            Console.WriteLine($"📥 Error: {body}");
            throw new HttpRequestException(
                BuildErrorMessage("obtener lotes", resp.StatusCode, body),
                null,
                resp.StatusCode);
        }

        return JsonSerializer.Deserialize<List<LoteDto>>(body, _json) ?? new();
    }

    public async Task<LoteDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        await EnsureAuthAsync();

        Console.WriteLine($"📤 GET /api/Lotes/{id}");

        using var resp = await _http.GetAsync($"{Resource}/{id}", ct);
        Console.WriteLine($"📥 StatusCode: {resp.StatusCode}");

        if (resp.StatusCode == HttpStatusCode.NotFound) return null;

        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<LoteDto>(_json, ct);
    }

    public async Task<List<LoteDto>> GetByProductIdAsync(int id, CancellationToken ct = default)
    {
        await EnsureAuthAsync();

        using var resp = await _http.GetAsync($"{Resource}/producto/{id}", ct);
        var body = await resp.Content.ReadAsStringAsync(ct);

        if (resp.StatusCode == HttpStatusCode.NotFound)
            return new List<LoteDto>();

        if (!resp.IsSuccessStatusCode)
            throw new HttpRequestException(
                BuildErrorMessage($"obtener lotes del producto {id}", resp.StatusCode, body),
                null,
                resp.StatusCode);

        return JsonSerializer.Deserialize<List<LoteDto>>(body, _json) ?? new();
    }
}
