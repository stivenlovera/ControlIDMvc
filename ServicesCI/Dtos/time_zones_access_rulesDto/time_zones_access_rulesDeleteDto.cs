using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.time_zones_access_rulesDto
{
    public class time_zones_access_rulesDeleteDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class time_zones_access_rulesResponseDeleteDto
    {
        public int changes { get; set; }
    }
}