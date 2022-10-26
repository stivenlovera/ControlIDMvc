using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.access_rulesDto
{
    public class accessRulesDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int type { get; set; }
        public int priority { get; set; }
    }
    public class AccessRuleResponseDto
    {
        public List<accessRulesDto> accessRulesDtos { get; set; }
    }
}