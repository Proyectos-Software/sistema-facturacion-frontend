using System;

namespace sistemaFacturacion.Models.Auditoria
{
    public class ResumenUsuarioDto
    {
        public string Usuario { get; set; }
        public int Inserts { get; set; }
        public int Updates { get; set; }
        public int Deletes { get; set; }
        public int Total { get; set; }
        public DateTime UltimaActividad { get; set; }
    }
}
