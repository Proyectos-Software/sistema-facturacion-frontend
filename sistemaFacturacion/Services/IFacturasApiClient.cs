using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using sistemaFacturacion.Models;

namespace sistemaFacturacion.Services
{
    public interface IFacturasApiClient
    {
        Task<List<FacturaDto>> GetAllAsync(CancellationToken ct = default);
        Task<FacturaDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<FacturaDto?> CreateAsync(CrearFacturaRequest request, CancellationToken ct = default);


        Task<bool> CambiarEstadoAsync(int id, string nuevoEstado, CancellationToken ct = default);
    }
}
