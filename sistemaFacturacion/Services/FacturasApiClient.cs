using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using sistemaFacturacion.Models;

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

    public async Task<FacturaDto> CrearFacturaAsync(FacturaCreateRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/Facturas", request);
        response.EnsureSuccessStatusCode();

        var factura = await response.Content.ReadFromJsonAsync<FacturaDto>();
        return factura!;
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
