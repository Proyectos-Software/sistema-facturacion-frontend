using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using sistemaFacturacion.Models;

namespace sistemaFacturacion.Services;

public class ProductosApiClient : IProductosApiClient
{
    private readonly HttpClient _http;
    private readonly SessionService _sessionService;

    private const string Resource = "api/Productos";

    private static readonly JsonSerializerOptions _json = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public ProductosApiClient(HttpClient http, SessionService sessionService)
    {
        _http = http;
        _sessionService = sessionService;
    }

    // Reutiliza el seteo de token en todas las llamadas
    private async Task EnsureAuthAsync()
    {
        var token = await _sessionService.GetTokenAsync();
        if (!string.IsNullOrWhiteSpace(token))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<ProductoDto?> CrearAsync(ProductoCreateRequest dto, CancellationToken ct = default)
    {
        await EnsureAuthAsync();

        var json = JsonSerializer.Serialize(dto, _json);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var resp = await _http.PostAsync(Resource, content, ct);

        if (!resp.IsSuccessStatusCode)
        {
            var body = await resp.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException($"Error al crear producto: {body}");
        }

        return await resp.Content.ReadFromJsonAsync<ProductoDto>(_json, ct);
    }


    public async Task<List<ProductoDto>> GetAllAsync(CancellationToken ct = default)
    {
        await EnsureAuthAsync();

        using var resp = await _http.GetAsync(Resource, ct);

        if (!resp.IsSuccessStatusCode)
        {
            var body = await resp.Content.ReadAsStringAsync(ct);
            throw new HttpRequestException($"Error al obtener productos ({(int)resp.StatusCode}): {body}");
        }

        return await resp.Content.ReadFromJsonAsync<List<ProductoDto>>(_json, ct) ?? new();
    }

    // ➕ GET BY ID
    public async Task<ProductoDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        await EnsureAuthAsync();

        using var resp = await _http.GetAsync($"{Resource}/{id}", ct);
        if (resp.StatusCode == HttpStatusCode.NotFound) return null;

        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<ProductoDto>(_json, ct);
    }

    // ➕ UPDATE (PUT)
    public async Task<ProductoDto> UpdateAsync(int id, ProductoUpdateRequest dto, CancellationToken ct = default)
    {
        await EnsureAuthAsync();

        var json = JsonSerializer.Serialize(dto, _json);
        using var resp = await _http.PutAsync($"{Resource}/{id}",
            new StringContent(json, Encoding.UTF8, "application/json"), ct);

        if (resp.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException($"Producto {id} no encontrado (404).");

        resp.EnsureSuccessStatusCode();
        return (await resp.Content.ReadFromJsonAsync<ProductoDto>(_json, ct))!;
    }


    public async Task<ProductoDto> UpdateEstadoAsync(int id, bool nuevoEstado, CancellationToken ct = default)
    {
        await EnsureAuthAsync();

        var url = $"{Resource}/{id}/estado?estado={nuevoEstado.ToString().ToLower()}";
        using var request = new HttpRequestMessage(HttpMethod.Patch, url);

        var response = await _http.SendAsync(request, ct);

        if (response.StatusCode == HttpStatusCode.NotFound)
            throw new HttpRequestException($"Producto {id} no encontrado (404).");

        response.EnsureSuccessStatusCode();
         
        var content = await response.Content.ReadAsStringAsync(ct); 
        return new ProductoDto
        {
            Id = id,
            Estado = nuevoEstado
        };
    }

}
