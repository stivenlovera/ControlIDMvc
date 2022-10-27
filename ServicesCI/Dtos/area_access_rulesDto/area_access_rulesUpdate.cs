using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.area_access_rulesDto
{
    public class area_access_rulesUpdate
    {
        public values values { get; set; }
        public where where { get; set; }
    }

    public class where
    {
        public area_access_rules area_Access_Rules { get; set; }
    }
    public class values
    {
        public string name { get; set; }
    }
    public class area_access_rules
    {
        public int area_id { get; set; }
    }
    public class area_access_rulesResponseUpdateDto
    {
        public int changes { get; set; }
    }
}