// File: Interfaces/IReportApiClients.cs (or similar location)

using System;
using System.IO;
using System.Threading.Tasks;

namespace sistemaFacturacion.Interfaces // Adjust namespace as needed
{
    /// <summary>
    /// Client for the "Ventas por Empleado" report API.
    /// </summary>
    public interface IVentasPorEmpleadoApiClient
    {
        /// <summary>
        /// Generates a PDF report of sales by employee within a specified date range.
        /// </summary>
        /// <param name="fechaInicio">Start date of the report (yyyy-MM-dd).</param>
        /// <param name="fechaFin">End date of the report (yyyy-MM-dd).</param>
        /// <returns>A Stream containing the PDF report.</returns>
        Task<Stream> GetVentasPorEmpleadoPdfAsync(DateTime fechaInicio, DateTime fechaFin);
    }

    /// <summary>
    /// Client for the general "Ventas" report API.
    /// </summary>
    public interface IVentasGeneralApiClient
    {
        /// <summary>
        /// Generates a general PDF report of all sales within a specified date range.
        /// </summary>
        /// <param name="inicio">Start date of the report (yyyy-MM-dd).</param>
        /// <param name="fin">End date of the report (yyyy-MM-dd).</param>
        /// <returns>A Stream containing the PDF report.</returns>
        Task<Stream> GetVentasGeneralPdfAsync(DateTime inicio, DateTime fin);
    }

    /// <summary>
    /// Client for the "Inventario" report API.
    /// </summary>
    public interface IInventarioApiClient
    {
        /// <summary>
        /// Generates a PDF report detailing products, lots, and inventory statistics.
        /// </summary>
        /// <returns>A Stream containing the PDF report.</returns>
        Task<Stream> GetInventarioPdfAsync();
    }

    /// <summary>
    /// Client for the "Dashboard Ventas" report API.
    /// </summary>
    public interface IDashboardVentasApiClient
    {
        /// <summary>
        /// Generates a PDF report of the sales dashboard KPIs, charts, and tables.
        /// </summary>
        /// <param name="inicio">Start date of the report (yyyy-MM-dd).</param>
        /// <param name="fin">End date of the report (yyyy-MM-dd).</param>
        /// <param name="idEmpresa">The company ID for the report.</param>
        /// <returns>A Stream containing the PDF report.</returns>
        Task<Stream> GetDashboardVentasPdfAsync(DateTime inicio, DateTime fin, int idEmpresa);
    }
}