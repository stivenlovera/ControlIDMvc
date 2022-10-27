using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.areasDto
{
    public class areaDeleteDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class areaResponseDeleteDto
    {
        public int changes { get; set; }
    }
}
