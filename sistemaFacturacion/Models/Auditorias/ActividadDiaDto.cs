using System;

namespace sistemaFacturacion.Models.Auditoria
{
    public class ActividadDiaDto
    {
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
        public int Inserts { get; set; }
        public int Updates { get; set; }
        public int Deletes { get; set; }
    }
}
