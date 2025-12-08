using System;

namespace sistemaFacturacion.Models.Auditoria
{
    public class CambioManualDto
    {
        public DateTime Fecha { get; set; }
        public string Tabla { get; set; }
        public int? RegistroId { get; set; }
        public string Accion { get; set; }
        public string LoginSql { get; set; }
        public string Host { get; set; }
        public string AppSql { get; set; }
        public string Detalle { get; set; }
    }
}
