using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IClientesApiClient
{
    Task<List<ClienteDto>> GetAllAsync(CancellationToken ct = default);
    Task<ClienteDto?> GetByIdAsync(int id, CancellationToken ct = default); 
    Task<ClienteDto> CreateAsync(ClienteCreateRequest request, CancellationToken ct = default);
    Task UpdateContactoAsync(int id, ClienteContactoUpdate request, CancellationToken ct = default);
    Task<ClienteDto?> GetByDocumentoAsync(string tipo, string identificacion, CancellationToken ct = default);
}
