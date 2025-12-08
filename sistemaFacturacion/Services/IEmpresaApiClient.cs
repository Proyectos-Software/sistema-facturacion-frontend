using sistemaFacturacion.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace sistemaFacturacion.Services
{
    public interface IEmpresaApiClient
    {
        Task<EmpresaDto?> GetAsync();
        Task<EmpresaDto> UpdateConfigBasicaAsync(int id, EmpresaConfigBasicaRequest request);
        Task<EmpresaDto> UpdateConfigSriAsync(int id, EmpresaConfigSriRequest request);
        Task<CertificadoEstadoDto> GetEstadoCertificadoAsync(int id);
        Task<bool> UploadCertificadoAsync(int id, CertificadoUploadRequest request);
    }

    public class EmpresaApiClient : IEmpresaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        private const string BaseEndpoint = "/api/EmpresaConfig";

        public EmpresaApiClient(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
        }


        /// <summary>
        /// GET /api/EmpresaConfig/{id}
        /// Obtener configuración de empresa
        /// </summary>
        public async Task<EmpresaDto?> GetAsync()
        {
            try
            {
                var empresaId = await _authService.GetCurrentEmpresaIdAsync();
                if (empresaId == null)
                    throw new UnauthorizedAccessException("No se pudo obtener ID de empresa del token.");

                var response = await _httpClient.GetAsync($"{BaseEndpoint}/{empresaId}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error al obtener empresa: {response.StatusCode}");
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<EmpresaDto>>();

                if (result?.Success == true)
                {
                    return result.Data;
                }

                throw new HttpRequestException(result?.Message ?? "Error al obtener empresa");
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Error de conexión: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// PUT /api/EmpresaConfig/{id}/configuracion-basica
        /// Actualizar configuración básica de empresa
        /// </summary>
        public async Task<EmpresaDto> UpdateConfigBasicaAsync(int id, EmpresaConfigBasicaRequest request)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{id}/configuracion-basica", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error al actualizar empresa: {response.StatusCode} - {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<EmpresaDto>>();

                if (result?.Success == true && result.Data != null)
                {
                    return result.Data;
                }

                throw new HttpRequestException(result?.Message ?? "Error al actualizar empresa");
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Error de conexión: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// PUT /api/EmpresaConfig/{id}/configuracion-sri
        /// Configurar parámetros del SRI
        /// </summary>
        public async Task<EmpresaDto> UpdateConfigSriAsync(int id, EmpresaConfigSriRequest request)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{id}/configuracion-sri", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Error al configurar SRI: {response.StatusCode} - {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<EmpresaDto>>();

                if (result?.Success == true && result.Data != null)
                {
                    return result.Data;
                }

                throw new HttpRequestException(result?.Message ?? "Error al configurar SRI");
            }
            catch (HttpRequestException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Error de conexión: {ex.Message}", ex);
            }
        }

        public async Task<CertificadoEstadoDto> GetEstadoCertificadoAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseEndpoint}/{id}/certificado/estado");

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error al verificar certificado: {response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                var success = root.GetProperty("success").GetBoolean();
                if (!success)
                    return new CertificadoEstadoDto { Existe = false };

                var data = root.GetProperty("data");

                var dto = new CertificadoEstadoDto
                {
                    Existe = data.TryGetProperty("tieneCertificado", out var tc) ? tc.GetBoolean() : false,
                    RutaCertificado = data.TryGetProperty("rutaRelativa", out var rr) ? rr.GetString() : null,
                    FechaVencimiento = data.TryGetProperty("fechaVencimiento", out var fv) ? fv.GetDateTime() : null,
                    DiasRestantes = data.TryGetProperty("diasParaVencer", out var dpv) ? dpv.GetInt32() : 0,
                    EstaVencido = data.TryGetProperty("estado", out var est) && est.GetString() == "VENCIDO",
                    Mensaje = data.TryGetProperty("estado", out var msj) ? msj.GetString() : "SIN ESTADO"
                };

                return dto;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Error al verificar certificado: {ex.Message}", ex);
            }
        }


        /// <summary>
        /// POST /api/EmpresaConfig/{id}/certificado
        /// Subir certificado digital (.p12 o .pfx)
        /// </summary>
        public async Task<bool> UploadCertificadoAsync(int id, CertificadoUploadRequest request)
        {
            try
            {
                using var content = new MultipartFormDataContent();

                var fileContent = new ByteArrayContent(request.ArchivoBytes);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-pkcs12");
                content.Add(fileContent, "Certificado", request.NombreArchivo);

                content.Add(new StringContent(request.Password), "ClaveCertificado");

                // ✅ Agregar fecha de vencimiento
                if (request.FechaVencimiento.HasValue)
                {
                    content.Add(
                        new StringContent(
                            request.FechaVencimiento.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                        ),
                        "FechaVencimiento"
                    );
                }

                var response = await _httpClient.PostAsync($"{BaseEndpoint}/{id}/certificado", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException(
                        $"Error al subir certificado: {response.StatusCode} - {errorContent}");
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
                return result?.Success == true;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException($"Error al subir certificado: {ex.Message}", ex);
            }
        }


    }

    // Clase para el wrapper de respuesta de la API
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}