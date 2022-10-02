using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Accion
{
    public class AccionCreateDto
    {
        public string Nombre { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }
        public string ControlIdAction { get; set; }
        public string ControlIdParametrers { get; set; }
        public int ControlIdRunAt { get; set; }
    }
}