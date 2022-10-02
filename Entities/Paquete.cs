using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Paquete
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Dias { get; set; }
        public DateTime FechaCreacion { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Costo { get; set; }
        public List<Inscripcion> Inscripciones { get; set; }
    }
}