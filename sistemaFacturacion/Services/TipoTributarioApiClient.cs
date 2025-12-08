using System.Net.Http;
using System.Net.Http.Json;
using sistemaFacturacion.Models;

namespace sistemaFacturacion.Services
{
    public class TipoTributarioApiClient : ITipoTributarioApiClient
    {
        private readonly HttpClient _http;

        public TipoTributarioApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TipoTributarioDto>> ListarAsync()
        {
            var response = await _http.GetAsync("api/tipo-tributario");
            response.EnsureSuccessStatusCode();

            var tipos = await response.Content.ReadFromJsonAsync<List<TipoTributarioDto>>();

            return tipos ?? new List<TipoTributarioDto>();
        }
    }
}
