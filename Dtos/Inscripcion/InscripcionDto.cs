using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.Persona;

namespace ControlIDMvc.Dtos.Inscripcion
{
    public class InscripcionDto
    {
        public int Id { get; set; }
        public string NumeroRecibo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal Costo { get; set; }
        public int PaqueteId { get; set; }
        public int PersonaId { get; set; }
        public PersonaDto Persona { get; set; }
        public string Cliente { get; set; }
        public int Dias { get; set; }
    }
}