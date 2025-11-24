// Models/Facturas/FacturaCreateRequest.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sistemaFacturacion.Models
{
    /// <summary>
    /// Request para crear la factura, firmarla y enviarla al SRI.
    /// </summary>
    public class FacturaCreateRequest
    {
        [Required]
        public int IdCli { get; set; }      // Cliente

        [Required]
        public int IdEmp { get; set; }      // Empleado (quien atiende)

        [Required]
        public int IdUsu { get; set; }     // Usuario del sistema que genera la factura

        [Required]
        public int IdEmpresa { get; set; }  // Empresa emisora

        /// <summary>
        /// Lista de productos seleccionados (FIFO según lotes).
        /// </summary>
        [MinLength(1, ErrorMessage = "Debe incluir al menos un detalle.")]
        public List<FacturaDetalleCreateRequest> Detalles { get; set; }
            = new();
    }

    /// <summary>
    /// Detalle de factura para creación (id del producto y cantidad).
    /// </summary>
    public class FacturaDetalleCreateRequest
    {
        [Required]
        public int IdPro { get; set; }      // Producto

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        public int Cantidad { get; set; }   // Cantidad solicitada
    }
}
