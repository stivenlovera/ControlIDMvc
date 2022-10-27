using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.time_zones_access_rulesDto
{
    public class time_zones_access_rulesUpdateDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class where
    {
        public time_zones_access_rules time_Zones_Access_Rules { get; set; }
    }
    public class values
    {
        public int access_rule_id { get; set; }
        public int time_zone_id { get; set; }

    }
    public class time_zones_access_rules
    {
        public int id { get; set; }
    }
    public class time_zones_access_rulesResponseUpdateDto
    {
        public int changes { get; set; }
    }
}