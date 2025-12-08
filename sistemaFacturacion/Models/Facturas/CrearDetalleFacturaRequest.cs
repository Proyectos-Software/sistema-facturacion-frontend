namespace sistemaFacturacion.Models;

public class CrearDetalleFacturaRequest
{
    public int IdPro { get; set; }
    public int IdLot { get; set; }
    public int Cantidad { get; set; }
}
