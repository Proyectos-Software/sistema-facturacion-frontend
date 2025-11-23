using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using sistemaFacturacion.Models;

namespace sistemaFacturacion.Services
{
    public class FacturasApiClient : IFacturasApiClient
    {
        private readonly HttpClient _http;
        private readonly SessionService _sessionService;

        private const string Resource = "api/Facturas";

        private static readonly JsonSerializerOptions _json = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public FacturasApiClient(HttpClient http, SessionService sessionService)
        {
            _http = http;
            _sessionService = sessionService;
        }

        // ============================================================
        //   TOKEN
        // ============================================================
        private async Task EnsureAuthAsync()
        {
            var token = await _sessionService.GetTokenAsync();
            if (!string.IsNullOrWhiteSpace(token))
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // ============================================================
        //   GET ALL
        // ============================================================
        public async Task<List<FacturaDto>> GetAllAsync(CancellationToken ct = default)
        {
            await EnsureAuthAsync();

            using var resp = await _http.GetAsync(Resource, ct);

            if (!resp.IsSuccessStatusCode)
            {
                var body = await resp.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException($"Error al obtener facturas ({(int)resp.StatusCode}): {body}");
            }

            var parsed = await resp.Content.ReadFromJsonAsync<List<FacturaDto>>(_json, ct);
            return parsed ?? new();
        }

        // ============================================================
        //   GET BY ID
        // ============================================================
        public async Task<FacturaDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            await EnsureAuthAsync();

            using var resp = await _http.GetAsync($"{Resource}/{id}", ct);

            if (resp.StatusCode == HttpStatusCode.NotFound)
                return null;

            resp.EnsureSuccessStatusCode();

            return await resp.Content.ReadFromJsonAsync<FacturaDto>(_json, ct);
        }

        // ============================================================
        //   CREATE (POST)
        // ============================================================
        public async Task<FacturaDto?> CreateAsync(CrearFacturaRequest request, CancellationToken ct = default)
        {
            await EnsureAuthAsync();

            var json = JsonSerializer.Serialize(request, _json);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var resp = await _http.PostAsync(Resource, content, ct);

            var body = await resp.Content.ReadAsStringAsync(ct);

            if (resp.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new HttpRequestException($"Datos inválidos: {body}", null, resp.StatusCode);
            }

            if (!resp.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Error al crear factura ({(int)resp.StatusCode}): {body}",
                    null,
                    resp.StatusCode
                );
            }

            // El backend retorna FacturaResponseDto => FacturaDto
            return JsonSerializer.Deserialize<FacturaDto>(body, _json);
        }

        // ============================================================
        //   CAMBIAR ESTADO
        // ============================================================
        public async Task<bool> CambiarEstadoAsync(int id, string nuevoEstado, CancellationToken ct = default)
        {
            await EnsureAuthAsync();

            var url = $"{Resource}/{id}/estado?estado={nuevoEstado}";
            using var request = new HttpRequestMessage(HttpMethod.Patch, url);

            using var resp = await _http.SendAsync(request, ct);

            if (resp.StatusCode == HttpStatusCode.NotFound)
                return false;

            resp.EnsureSuccessStatusCode();
            return true;
        }
    }
}
