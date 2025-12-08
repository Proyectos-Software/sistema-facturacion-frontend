using sistemaFacturacion.Models.Auditoria;

public interface IAuditoriaApiClient
{
    Task<EstadisticasGeneralesDto> GetEstadisticas();
    Task<List<ActividadDiaDto>> GetActividadDia();

    // 🆕 NUEVO
    Task<List<ActividadHoraDto>> GetActividadHora();

    Task<List<ResumenTablaDto>> GetResumenTablas();
    Task<List<CambioManualDto>> GetCambiosManuales(DateTime? desde, DateTime? hasta);
    Task<List<DeleteDto>> GetDeletes(string? tabla, DateTime? desde, DateTime? hasta);

    Task<List<HistorialRegistroDto>> GetHistorialRegistroAsync(string tabla, int id);
    Task<List<ResumenUsuarioDto>> GetResumenPorUsuariosAsync();
}
