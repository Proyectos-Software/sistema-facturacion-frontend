using System.Linq;

namespace sistemaFacturacion.Models;

public static class FacturaDraftExtensions
{
    public static CrearFacturaRequest ToCreateRequest(this FacturaDraft draft)
    {
        return new CrearFacturaRequest
        {
            IdCli = draft.IdCliente!.Value,
            IdEmp = draft.IdEmpleado!.Value,
            IdUsu = draft.IdUsuario!.Value,

            Detalles = draft.Detalles.Select(d => new CrearDetalleFacturaRequest
            {
                IdPro = d.IdProducto,
                IdLot = d.IdLote,
                Cantidad = d.Cantidad
            }).ToList()
        };
    }
}
