using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.PlanCuentaCompuesta
{
    public class PlanCuentaCompuestaCreateDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Nivel { get; set; }
        public int PlanCuentaTituloId { get; set; }
    }
}