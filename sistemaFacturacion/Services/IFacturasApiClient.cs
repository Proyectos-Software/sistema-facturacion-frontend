using System.Collections.Generic;
using System.Threading.Tasks;
using sistemaFacturacion.Models;

public interface IFacturasApiClient
{
    Task<List<FacturaDto>> GetFacturasAsync();
    Task<FacturaDto?> GetFacturaPorIdAsync(int id);
    Task<FacturaDto> CrearFacturaAsync(FacturaCreateRequest request);

    // Cambiar estado de una factura
    Task<bool> CambiarEstadoAsync(int idFactura, string nuevoEstado);

    // Opcional: XML
    Task<string> ObtenerXmlAsync(int id);
    Task<byte[]> DescargarXmlAsync(int id);
}

