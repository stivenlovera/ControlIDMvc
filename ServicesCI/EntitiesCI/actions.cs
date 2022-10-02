using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.EntitiesCI
{
    public class actions
    {
        public int id { get; set; }
        public string name { get; set; }
        public string action { get; set; }
        public string parameters { get; set; }
        public int run_at { get; set; }
    }
}