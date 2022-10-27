using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.area_access_rulesDto
{
    public class area_access_rulesControlDto
    {
        public int area_id { get; set; }
        public int access_rule_id { get; set; }
    }
    public class area_access_rulesResponseDto
    {
        public List<area_access_rulesControlDto> area_Access_RulesControlDtos { get; set; }
    }
}