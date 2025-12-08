using System;

namespace sistemaFacturacion.Models.Auditoria
{
    public class ResumenTablaDto
    {
        public string Tabla { get; set; }
        public int Total { get; set; }
        public int Inserts { get; set; }
        public int Updates { get; set; }
        public int Deletes { get; set; }
        public DateTime UltimaActividad { get; set; }
    }
}
