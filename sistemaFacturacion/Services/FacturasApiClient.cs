using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using sistemaFacturacion.Models;

namespace sistemaFacturacion.Services
{
    public class FacturasApiClient : IFacturasApiClient
    {
        private readonly HttpClient _http;

        public FacturasApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<FacturaDto>> GetFacturasAsync(CancellationToken ct = default)
        {
            var result = await _http.GetFromJsonAsync<List<FacturaDto>>("api/Facturas", ct);
            return result ?? new List<FacturaDto>();
        }

        public async Task<FacturaDto?> GetFacturaPorIdAsync(int id, CancellationToken ct = default)
        {
            return await _http.GetFromJsonAsync<FacturaDto>($"api/Facturas/{id}", ct);
        }

        /// <summary>
        /// Crea la factura, la firma y la envia al SRI (flujo completo).
        /// Usa el endpoint: POST api/Facturas/crear-firmar-enviar
        /// </summary>
        public async Task<FacturaDto> CrearFacturaAsync(FacturaCreateRequest request, CancellationToken ct = default)
        {
            var response = await _http.PostAsJsonAsync("api/Facturas/crear-firmar-enviar", request, ct);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException(
                    $"Error al crear la factura. Codigo {(int)response.StatusCode}: {errorBody}");
            }

            var factura = await response.Content.ReadFromJsonAsync<FacturaDto>(cancellationToken: ct);

            if (factura is null)
                throw new HttpRequestException("El backend devolvio una respuesta vacia al crear la factura.");

            return factura;
        }

        // Cambiar estado de la factura (anular, marcar como enviada, etc.)
        public async Task<bool> CambiarEstadoAsync(int idFactura, string nuevoEstado, CancellationToken ct = default)
        {
            var body = new { estado = nuevoEstado };

            var response = await _http.PutAsJsonAsync($"api/Facturas/{idFactura}/estado", body, ct);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Reenvia una factura al SRI para autorizacion
        /// Ubicacion: Services/FacturasApiClient.cs
        /// Efecto en el Front: Permite reintentar la autorizacion de facturas en estado FIRMADO
        /// Endpoint: POST api/Sri/enviar/{idFactura}
        /// </summary>
        public async Task<SriEnvioResponse> ReenviarFacturaAsync(int idFactura, CancellationToken ct = default)
        {
            var response = await _http.PostAsync($"api/Sri/enviar/{idFactura}", null, ct);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(ct);
                throw new HttpRequestException(
                    $"Error al reenviar factura al SRI. Codigo {(int)response.StatusCode}: {errorBody}");
            }

            var result = await response.Content.ReadFromJsonAsync<SriEnvioResponse>(cancellationToken: ct);

            if (result is null)
                throw new HttpRequestException("El backend devolvio una respuesta vacia al reenviar la factura.");

            return result;
        }

        public async Task<string> ObtenerXmlAsync(int id, CancellationToken ct = default)
        {
            // devuelve string XML en texto
            var response = await _http.GetAsync($"api/Facturas/{id}/xml", ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(ct);
        }

        public async Task<byte[]> DescargarXmlAsync(int id, CancellationToken ct = default)
        {
            var response = await _http.GetAsync($"api/Facturas/{id}/xml/download", ct);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync(ct);
        }
    }
}
