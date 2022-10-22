using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DatatableInscripciones
    {
        public int Id { get; set; }
        public string numeroRecibo { get; set; }
        public string FechaCreacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal PaqueteCosto { get; set; }
        public int PaqueteId { get; set; }
        public string PaqueteNombre { get; set; }
        public string PersonaNombre { get; set; }
        public string PersonaCi { get; set; }
        public int PaqueteDias { get; set; }
        public int PersonaId { get; set; }
    }
}