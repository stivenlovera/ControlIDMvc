using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.time_zonesDto
{
    public class timezoneDelete
    {
        public class timezoneDeleteDto
        {
            public values values { get; set; }
            public where where { get; set; }
        }
        public class timezoneResponseDeleteDto
        {
            public int changes { get; set; }
        }
    }
}