using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<List<FacturaDto>> GetFacturasAsync()
        {
            var result = await _http.GetFromJsonAsync<List<FacturaDto>>("api/Facturas");
            return result ?? new List<FacturaDto>();
        }

        public async Task<FacturaDto?> GetFacturaPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<FacturaDto>($"api/Facturas/{id}");
        }

        /// <summary>
        /// Crea la factura, la firma y la envía al SRI (flujo completo).
        /// Usa el endpoint: POST api/Facturas/crear-firmar-enviar
        /// </summary>
        public async Task<FacturaDto> CrearFacturaAsync(FacturaCreateRequest request)
        {
            // 👇 Aquí cambiamos la ruta al nuevo endpoint del Swagger
            var response = await _http.PostAsJsonAsync("api/Facturas/crear-firmar-enviar", request);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Error al crear la factura. Código {(int)response.StatusCode}: {errorBody}");
            }

            var factura = await response.Content.ReadFromJsonAsync<FacturaDto>();

            if (factura is null)
                throw new HttpRequestException("El backend devolvió una respuesta vacía al crear la factura.");

            return factura;
        }

        // 🔹 Cambiar estado de la factura (anular, marcar como enviada, etc.)
        public async Task<bool> CambiarEstadoAsync(int idFactura, string nuevoEstado)
        {
            var body = new { estado = nuevoEstado };

            var response = await _http.PutAsJsonAsync($"api/Facturas/{idFactura}/estado", body);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> ObtenerXmlAsync(int id)
        {
            // devuelve string XML en texto
            return await _http.GetStringAsync($"api/Facturas/{id}/xml");
        }

        public async Task<byte[]> DescargarXmlAsync(int id)
        {
            return await _http.GetByteArrayAsync($"api/Facturas/{id}/xml/download");
        }
    }
}
