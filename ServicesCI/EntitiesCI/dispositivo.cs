using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.EntitiesCI
{
    public class dispositivo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string ip { get; set; }
        public string public_key { get; set; }
    }
}