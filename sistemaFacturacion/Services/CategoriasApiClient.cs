using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using sistemaFacturacion.Models;

namespace sistemaFacturacion.Services
{
    public class CategoriasApiClient : ICategoriasApiClient
    {
        private readonly HttpClient _http;

        public CategoriasApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CategoriaProductoDto>> ListarAsync(CancellationToken ct = default)
        {
            var response = await _http.GetAsync("api/categorias", ct);
            response.EnsureSuccessStatusCode();

            var categorias = await response.Content.ReadFromJsonAsync<List<CategoriaProductoDto>>(cancellationToken: ct);

            return categorias ?? new List<CategoriaProductoDto>();
        }
    }
}
