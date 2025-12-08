using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using sistemaFacturacion.Models;

public interface IFacturasApiClient
{
    Task<List<FacturaDto>> GetFacturasAsync(CancellationToken ct = default);
    Task<FacturaDto?> GetFacturaPorIdAsync(int id, CancellationToken ct = default);
    Task<FacturaDto> CrearFacturaAsync(FacturaCreateRequest request, CancellationToken ct = default);

    // Cambiar estado de una factura
    Task<bool> CambiarEstadoAsync(int idFactura, string nuevoEstado, CancellationToken ct = default);

    // Reenviar factura al SRI para autorizacion
    Task<SriEnvioResponse> ReenviarFacturaAsync(int idFactura, CancellationToken ct = default);

    // Opcional: XML
    Task<string> ObtenerXmlAsync(int id, CancellationToken ct = default);
    Task<byte[]> DescargarXmlAsync(int id, CancellationToken ct = default);
}

