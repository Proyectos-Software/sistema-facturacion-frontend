using Microsoft.AspNetCore.Components;

namespace sistemaFacturacion.Models
{
    // Clase para KPI Card en el dashboard
    public class KpiCard
    {
        public string Title { get; set; } = "";
        public string Value { get; set; } = "";
        public string Sub { get; set; } = "";
        public string BadgeClass { get; set; } = "success";
        public string SvgColor { get; set; } = "primary";
        public MarkupString IconSvg { get; set; }
    }

    // Clase para representar puntos de ventas localmente
    public class SalesPoint
    {
        public string BucketLabel { get; set; } = "";
        public decimal Ventas { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public int FacturasAut { get; set; }
    }

    // Clase para productos top
    public class TopProduct
    {
        public string Nombre { get; set; } = "";
        public int Cantidad { get; set; }
    }

    // Clase para alertas de stock
    public class StockAlert
    {
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Stock { get; set; }
        public int Minimo { get; set; }
    }

    // Clase para filas de facturas
    public class InvoiceRow
    {
        public string Numero { get; set; } = "";
        public string Cliente { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string EstadoSRI { get; set; } = "";
        public decimal Total { get; set; }
    }
}