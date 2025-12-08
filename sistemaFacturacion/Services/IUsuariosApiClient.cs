using sistemaFacturacion.Models;
public interface IUsuariosApiClient
{
    Task<IReadOnlyList<UsuarioDto>> GetAllAsync(CancellationToken ct = default);
    Task<UsuarioDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<UsuarioDto> CreateEmpleadoAsync(CreateEmpleadoRequest request, CancellationToken ct = default);
    Task<UsuarioDto> UpdateDatosAsync(UpdateUsuarioRequest request, CancellationToken ct = default);
    Task<UsuarioDto> UpdateDatosByIdAsync(int id, UpdateUsuarioRequest request, CancellationToken ct = default);
    Task<bool> CambiarContrasenaAsync(ChangePasswordRequest request, CancellationToken ct = default);
}
