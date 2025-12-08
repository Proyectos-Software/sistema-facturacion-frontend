using sistemaFacturacion.Models;

  
public interface ILotesApiClient
{
    Task<LoteDto?> CreateAsync(LoteCreateRequest dto, CancellationToken ct = default);
    Task<LoteDto> UpdateAsync(int id, LoteUpdateRequest dto, CancellationToken ct = default);
    Task<List<LoteDto>> GetAllAsync(CancellationToken ct = default);
    Task<LoteDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<LoteDto>> GetByProductIdAsync(int id, CancellationToken ct=default);

}
    