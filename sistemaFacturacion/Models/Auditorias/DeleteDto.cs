using System;

namespace sistemaFacturacion.Models.Auditoria
{
    public class DeleteDto
    {
        public DateTime Fecha { get; set; }
        public string Tabla { get; set; }
        public int? RegistroId { get; set; }
        public string Usuario { get; set; }
        public string Host { get; set; }
        public string Detalle { get; set; }
        public string LoginSql { get; set; }
    }
}
