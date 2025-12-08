// File: Services/ReportApiClient.cs (or similar location)

using sistemaFacturacion.Interfaces;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json; // For potential JSON responses if needed, though here we expect streams
using System.Text.Json;
using System.Threading.Tasks;
using sistemaFacturacion.Interfaces; // Adjust namespace as needed

namespace sistemaFacturacion.Services // Adjust namespace as needed
{
    public class ReportApiClient : IVentasPorEmpleadoApiClient, IVentasGeneralApiClient, IInventarioApiClient, IDashboardVentasApiClient
    {
        private readonly HttpClient _httpClient;
        // private const string BaseApiUrl = "https://localhost:7084/"; // Your base URL from example

        // JsonSerializerOptions for consistent JSON handling if needed for other calls
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        // Constructor accepting HttpClient. IHttpClientFactory is generally preferred.
        // Make sure this HttpClient is configured with the correct BaseAddress in Program.cs
        public ReportApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            // Ensure BaseAddress is set if not done via AddHttpClient in Program.cs
            // Example: if _httpClient is registered without a specific name and base address.
            // If using AddHttpClient("ReportApi", ...), the base address is handled there.
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:7084/"); // <<< IMPORTANT: Set your actual base URL here if not configured elsewhere.
            }
        }

        // --- Ventas por Empleado Report ---
        public async Task<Stream> GetVentasPorEmpleadoPdfAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            // Format dates as yyyy-MM-dd as per API spec
            var formattedFechaInicio = fechaInicio.ToString("yyyy-MM-dd");
            var formattedFechaFin = fechaFin.ToString("yyyy-MM-dd");

            var requestUri = $"api/reportes/ventas-por-empleado/pdf?fechaInicio={formattedFechaInicio}&fechaFin={formattedFechaFin}";

            // Use GetStreamAsync for direct stream retrieval
            var responseStream = await _httpClient.GetStreamAsync(requestUri);
            return responseStream;
        }

        // --- Ventas Generales Report ---
        public async Task<Stream> GetVentasGeneralPdfAsync(DateTime inicio, DateTime fin)
        {
            var formattedInicio = inicio.ToString("yyyy-MM-dd");
            var formattedFin = fin.ToString("yyyy-MM-dd");

            var requestUri = $"api/reportes/ventas?inicio={formattedInicio}&fin={formattedFin}";

            var responseStream = await _httpClient.GetStreamAsync(requestUri);
            return responseStream;
        }

        // --- Inventario Report ---
        public async Task<Stream> GetInventarioPdfAsync()
        {
            var requestUri = "api/reportes/inventario/pdf";

            var responseStream = await _httpClient.GetStreamAsync(requestUri);
            return responseStream;
        }

        // --- Dashboard Ventas Report ---
        public async Task<Stream> GetDashboardVentasPdfAsync(DateTime inicio, DateTime fin, int idEmpresa)
        {
            var formattedInicio = inicio.ToString("yyyy-MM-dd");
            var formattedFin = fin.ToString("yyyy-MM-dd");

            // Construct the URL with all parameters
            var requestUri = $"api/reportes/dashboard/ventas/pdf?inicio={formattedInicio}&fin={formattedFin}&idEmpresa={idEmpresa}";

            var responseStream = await _httpClient.GetStreamAsync(requestUri);
            return responseStream;
        }
    }
}