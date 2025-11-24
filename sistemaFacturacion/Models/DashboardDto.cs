using Microsoft.AspNetCore.Components;

namespace sistemaFacturacion.Models
{
    // DTO principal que retorna el dashboard
    public class DashboardOverviewDto
    {
        public List<KpiDto> Kpis { get; set; } = new();
        public List<SalesPointDto> Sales { get; set; } = new();
        public List<TopProductDto> TopProductos { get; set; } = new();
        public List<StockAlertDto> AlertasStock { get; set; } = new();
        public List<InvoiceRowDto> UltimasFacturas { get; set; } = new();
    }

    // KPIs principales
    public class KpiDto
    {
        public string Title { get; set; } = "";
        public decimal Value { get; set; }
        public string FormattedValue { get; set; } = "";
        public decimal? PreviousValue { get; set; }
        public decimal? ChangePercent { get; set; }
        public string ChangeText { get; set; } = "";
        public string BadgeClass { get; set; } = "success";
        public string Type { get; set; } = ""; // ventas, facturas, rechazadas, iva
    }

    // Punto de datos para el gráfico de ventas
    public class SalesPointDto
    {
        public string BucketLabel { get; set; } = "";
        public decimal Ventas { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public int FacturasAut { get; set; }
    }

    // Top productos más vendidos
    public class TopProductDto
    {
        public string Nombre { get; set; } = "";
        public int Cantidad { get; set; }
        public decimal MontoTotal { get; set; }
    }

    // Alertas de stock bajo
    public class StockAlertDto
    {
        public string Codigo { get; set; } = "";
        public string Nombre { get; set; } = "";
        public int Stock { get; set; }
        public int Minimo { get; set; }
    }

    // Últimas facturas
    public class InvoiceRowDto
    {
        public string Numero { get; set; } = "";
        public string Cliente { get; set; } = "";
        public DateTime Fecha { get; set; }
        public string EstadoSRI { get; set; } = "";
        public decimal Total { get; set; }
    }

    // Request para filtros del dashboard
    public class DashboardFiltersRequest
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? BranchId { get; set; }
        public string? SalesUserId { get; set; }
        public string? SriStatus { get; set; }
        public bool CompareWithPrevious { get; set; } = false;
        public string Bucket { get; set; } = "day"; // day, week, month
    }
}