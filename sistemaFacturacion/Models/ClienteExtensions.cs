
namespace sistemaFacturacion;
public static class ClienteExtensions
{
    public static ClienteDto ToClienteDto(this ClienteApiResponse response)
    {
        return new ClienteDto
        {
            IdCli = response.Id,
            NomCli = response.Nombre,
            ApeCli = response.Apellido,
            IdeCli = response.Identificacion,
            TipCli = response.TipoIdentificacion,
            DirCli = response.Direccion,
            TelCli = response.Telefono,
            CorCli = response.CorreoElectronico,
            FecCli = response.FechaCreacion,
            EstCli = response.Estado,
            CodCli = response.CodigoCliente
        };
    }

    public static List<ClienteDto> ToClienteDtoList(this IEnumerable<ClienteApiResponse> responses)
    {
        return responses.Select(r => r.ToClienteDto()).ToList();
    }
}