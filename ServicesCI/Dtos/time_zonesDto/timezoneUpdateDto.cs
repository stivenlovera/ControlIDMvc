using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.time_zonesDto
{
    public class timezoneUpdateDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }

    public class where
    {
        public time_zones time_zones { get; set; }
    }
    public class values
    {
        public string name { get; set; }

    }
    public class time_zones
    {
        public int id { get; set; }
    }
    public class timezoneResponseUpdateDto
    {
        public int changes { get; set; }
    }
}