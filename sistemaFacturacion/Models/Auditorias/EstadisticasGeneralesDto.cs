using System;

namespace sistemaFacturacion.Models.Auditoria
{
    public class EstadisticasGeneralesDto
    {
        public int TotalRegistros { get; set; }
        public int TotalInserts { get; set; }
        public int TotalUpdates { get; set; }
        public int TotalDeletes { get; set; }
        public int CambiosManuales { get; set; }
        public int UsuariosActivos { get; set; }
        public int TablasAuditadas { get; set; }
        public DateTime PrimerRegistro { get; set; }
        public DateTime UltimoRegistro { get; set; }
    }
}
