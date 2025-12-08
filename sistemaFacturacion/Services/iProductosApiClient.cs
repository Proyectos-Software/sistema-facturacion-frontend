using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using sistemaFacturacion.Models; 

    public interface IProductosApiClient
    {
        Task<List<ProductoDto>> GetAllAsync(CancellationToken ct = default);
        Task<ProductoDto?> CrearAsync(ProductoCreateRequest dto, CancellationToken ct = default);

        Task<ProductoDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<ProductoDto> UpdateAsync(int id, ProductoUpdateRequest dto, CancellationToken ct = default);
        Task<ProductoDto> UpdateEstadoAsync(int id, bool nuevoEstado, CancellationToken ct = default);


}

