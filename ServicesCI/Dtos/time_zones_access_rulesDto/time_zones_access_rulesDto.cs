using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.time_zones_access_rulesDto
{
    public class time_zones_access_rulesDto
    {
        public int access_rule_id { get; set; }
        public int time_zone_id { get; set; }
    }
     public class time_zones_access_rulesResponseDto
    {
        public List<time_zones_access_rulesDto> access_rule_time_zones { get; set; }
    }
}