using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.area_access_rulesDto
{
    public class area_access_rulesCreateDto
    {
        public int area_id { get; set; }
        public int access_rule_id { get; set; }
    }
    public class area_access_rulesResponseCreateDto
    {
        public List<int> ids { get; set; }
    }
}