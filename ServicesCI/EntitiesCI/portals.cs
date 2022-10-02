using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.EntitiesCI
{
    public class portals
    {
        public int id { get; set; }
        public string name { get; set; }
        public int area_from_id { get; set; }
        public int area_to_id { get; set; }
    }
}