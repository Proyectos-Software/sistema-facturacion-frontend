using System;

namespace sistemaFacturacion.Models.Auditoria
{
    public class HistorialRegistroDto
    {
        public DateTime Fecha { get; set; }
        public string Accion { get; set; }
        public string Detalle { get; set; }
        public string UsuarioApp { get; set; }
        public string LoginSql { get; set; }
        public string Host { get; set; }
        public string AppSql { get; set; }
    }
}
