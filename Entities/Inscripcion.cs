using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public string NumeroRecibo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Costo { get; set; }
        public int PaqueteId { get; set; }
        public Paquete Paquete { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
    }
}