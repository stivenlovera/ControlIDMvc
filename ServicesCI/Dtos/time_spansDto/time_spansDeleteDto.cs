using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.time_spansDto
{
    public class time_spansDeleteDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class time_spansResponseDeleteDto
    {
        public int changes { get; set; }
    }
}