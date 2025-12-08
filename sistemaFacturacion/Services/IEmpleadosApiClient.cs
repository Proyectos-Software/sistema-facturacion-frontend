using sistemaFacturacion.Models;
namespace sistemaFacturacion.Services;

public interface IEmpleadosApiClient
{   
    Task<IReadOnlyList<EmpleadoDto>> GetAllAsync(CancellationToken ct = default);
    Task<EmpleadoDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> UpdateAsync(int id, UpdateEmpleadoRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken ct = default);
}

