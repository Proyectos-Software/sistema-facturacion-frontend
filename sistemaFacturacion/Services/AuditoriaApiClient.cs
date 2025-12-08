using sistemaFacturacion.Models.Auditoria;
using System.Net.Http.Json;

public class AuditoriaApiClient : IAuditoriaApiClient
{
    private readonly HttpClient _http;

    public AuditoriaApiClient(HttpClient http)
    {
        _http = http;
    }

    public Task<EstadisticasGeneralesDto> GetEstadisticas()
        => _http.GetFromJsonAsync<EstadisticasGeneralesDto>("api/auditoria/estadisticas");

    public Task<List<ActividadDiaDto>> GetActividadDia()
        => _http.GetFromJsonAsync<List<ActividadDiaDto>>("api/auditoria/actividad/dia");

    public Task<List<ActividadHoraDto>> GetActividadHora()
        => _http.GetFromJsonAsync<List<ActividadHoraDto>>("api/auditoria/actividad/hora");

    public Task<List<ResumenTablaDto>> GetResumenTablas()
        => _http.GetFromJsonAsync<List<ResumenTablaDto>>("api/auditoria/tablas/resumen");

    public Task<List<ResumenUsuarioDto>> GetResumenPorUsuariosAsync()
        => _http.GetFromJsonAsync<List<ResumenUsuarioDto>>("api/auditoria/usuarios/resumen");

    public Task<List<CambioManualDto>> GetCambiosManuales(DateTime? desde, DateTime? hasta)
    {
        string url = "api/auditoria/manual";

        var query = new List<string>();

        if (desde.HasValue) query.Add($"desde={desde.Value:O}");
        if (hasta.HasValue) query.Add($"hasta={hasta.Value:O}");

        if (query.Any())
            url += "?" + string.Join("&", query);

        return _http.GetFromJsonAsync<List<CambioManualDto>>(url);
    }

    public Task<List<DeleteDto>> GetDeletes(string? tabla, DateTime? desde, DateTime? hasta)
    {
        var query = new List<string>();

        if (!string.IsNullOrWhiteSpace(tabla))
            query.Add($"tabla={tabla}");

        if (desde.HasValue)
            query.Add($"desde={desde.Value:O}");

        if (hasta.HasValue)
            query.Add($"hasta={hasta.Value:O}");

        string url = "api/auditoria/deletes";
        if (query.Any())
            url += "?" + string.Join("&", query);

        return _http.GetFromJsonAsync<List<DeleteDto>>(url);
    }

    public Task<List<HistorialRegistroDto>> GetHistorialRegistroAsync(string tabla, int id)
        => _http.GetFromJsonAsync<List<HistorialRegistroDto>>(
            $"api/auditoria/registro?tabla={tabla}&id={id}");
}
