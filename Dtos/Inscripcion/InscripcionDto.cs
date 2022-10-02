using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Inscripcion
{
    public class InscripcionDto
    {
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public double Costo { get; set; }
        public int PaqueteId { get; set; }
        public int PersonaId { get; set; }
        public string Cliente { get; set; }
        public int Dias { get; set; }
    }
}