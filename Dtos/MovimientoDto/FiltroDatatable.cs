using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.MovimientoDto
{
    public class FiltroDatatable
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int TipoMovimientoId { get; set; }
        public int PersonaId { get; set; }
    }
}