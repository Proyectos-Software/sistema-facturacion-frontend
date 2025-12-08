using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace sistemaFacturacion;
public class ClientesApiClient : IClientesApiClient
{
    private readonly HttpClient _http;
    private const string Endpoint = "api/Clientes";
    private static readonly JsonSerializerOptions _json = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public ClientesApiClient(HttpClient http) => _http = http;

    public async Task<List<ClienteDto>> GetAllAsync(CancellationToken ct = default)
    {
        var response = await _http.GetFromJsonAsync<List<ClienteApiResponse>>(Endpoint, _json, ct);

        return response?.ToClienteDtoList() ?? new List<ClienteDto>();
    }
    public async Task<ClienteDto?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<ClienteDto>($"{Endpoint}/{id}", _json, ct);

    public async Task<ClienteDto> CreateAsync(ClienteCreateRequest request, CancellationToken ct = default)
    {
        var resp = await _http.PostAsJsonAsync(Endpoint, request, _json, ct);
        await Ensure(resp);
        var body = await resp.Content.ReadFromJsonAsync<ClienteDto>(_json, ct);
        return body!;
    }

    public async Task UpdateContactoAsync(int id, ClienteContactoUpdate request, CancellationToken ct = default)
    {
        var resp = await _http.PutAsJsonAsync($"{Endpoint}/{id}", request, _json, ct);
        if (resp.StatusCode == HttpStatusCode.NoContent) return;
        await Ensure(resp);
    }
    public async Task<ClienteDto?> GetByDocumentoAsync(string tipo, string identificacion, CancellationToken ct = default)
    {
      
        string NT(string? s) => RemoveDiacritics((s ?? "").Trim()).ToUpperInvariant();
        string ND(string? s) => new string((s ?? "").Where(char.IsDigit).ToArray());

        var tip = Uri.EscapeDataString(NT(tipo));
        var doc = Uri.EscapeDataString(ND(identificacion));


        var url = $"{Endpoint}/by-documento?tipoIdentificacion={tip}&identificacion={doc}";
        using var resp = await _http.GetAsync(url, ct);

        if (resp.StatusCode == HttpStatusCode.NotFound) return null;
  
        await Ensure(resp);

        var body = await resp.Content.ReadAsStringAsync(ct);
        var opts = new JsonSerializerOptions(_json)
        {
            PropertyNameCaseInsensitive = true
        };

        try
        {
            var frontShape = JsonSerializer.Deserialize<ClienteDto>(body, opts);
            if (frontShape is not null && (frontShape.IdeCli?.Length ?? 0) > 0)
                return frontShape;
        }
        catch { }

        try
        {
            var apiShape = JsonSerializer.Deserialize<ClienteByDocApiDto>(body, opts);
            if (apiShape is null) return null;


            return new ClienteDto
            {
                IdCli = apiShape.Id ?? 0, 
                NomCli = apiShape.Nombre,
                ApeCli = apiShape.Apellido,
                IdeCli = apiShape.Identificacion,
                TipCli = apiShape.TipoIdentificacion,
                DirCli = apiShape.Direccion,
                TelCli = apiShape.Telefono,
                CorCli = apiShape.CorreoElectronico,
                FecCli = default,
                EstCli = true
            };
        }
        catch
        {

            return null;
        }
    }

    private sealed class ClienteByDocApiDto
    {
        public int? Id { get; set; }                    
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Identificacion { get; set; }
        public string? TipoIdentificacion { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? CorreoElectronico { get; set; }
    }

    private static async Task Ensure(HttpResponseMessage r)
    {
        if (r.IsSuccessStatusCode) return;
        var text = r.Content is null ? null : await r.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Error {(int)r.StatusCode} {r.ReasonPhrase}. {text}");
    }

    private static string RemoveDiacritics(string text)
    {
        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(capacity: normalized.Length);
        foreach (var ch in normalized)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
            if (uc != UnicodeCategory.NonSpacingMark) sb.Append(ch);
        }
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }

}
