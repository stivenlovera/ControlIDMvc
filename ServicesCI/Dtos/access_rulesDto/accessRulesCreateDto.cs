using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.access_rulesDto
{
    public class accessRulesCreateDto
    {
        public string name { get; set; }
        public int type { get; set; }
        public int priority { get; set; }
    }
    public class responseApiAccessRulesCreateDto
    {
        public List<int> ids { get; set; }
    }
}