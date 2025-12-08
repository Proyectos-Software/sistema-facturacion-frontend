using System.Collections.Generic;

namespace sistemaFacturacion.Models;

public class CrearFacturaRequest
{
    public int IdCli { get; set; }
    public int IdEmp { get; set; }
    public int IdUsu { get; set; }

    public List<CrearDetalleFacturaRequest> Detalles { get; set; } = new();
}
