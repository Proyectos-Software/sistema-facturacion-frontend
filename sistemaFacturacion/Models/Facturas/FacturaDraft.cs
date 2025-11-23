using System.Collections.Generic;

namespace sistemaFacturacion.Models
{
    public class FacturaDraft
    {
        // --- Cliente seleccionado ---
        public int? IdCliente { get; set; }
        public string NombreCliente { get; set; } = "";
        public string DocumentoCliente { get; set; } = "";
        public string DireccionCliente { get; set; } = "";
        public string CorreoCliente { get; set; } = "";  // <-- AGREGADO

        // --- Empleado / Usuario ---
        public int? IdEmpleado { get; set; }
        public int? IdUsuario { get; set; }

        // --- Totales ---
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }

        // --- Detalles agregados ---
        public List<DetalleFacturaDraft> Detalles { get; set; } = new();
    }

    public class DetalleFacturaDraft
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = "";

        public int IdLote { get; set; }
        public string CodigoLote { get; set; } = "";   // <-- AGREGADO

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
    }
}
