using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.AreaReglaAccesoCreateDto
{
    public class AreaReglaAccesoCreateDto
    {
        public int ControlIdAreaId { get; set; }
        public int ControlIdAccesoRulesId { get; set; }
        public int AreaId { get; set; }
        public int ReglaAccesoId { get; set; }
    }
}