using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.area_access_rulesDto
{
    public class area_access_rulesDelete
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class area_access_rulesResponseDeleteDto
    {
        public int changes { get; set; }
    }
}