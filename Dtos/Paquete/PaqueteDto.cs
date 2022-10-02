using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Paquete
{
    public class PaqueteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Dias { get; set; }
        public DateTime FechaCreacion { get; set; }
        public double Costo { get; set; }
    }
}