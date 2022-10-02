using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.portalsDto
{
    public class portalsCreateDto
    {
        public string name { get; set; }
        public int area_from_id { get; set; }
        public int area_to_id { get; set; }
    }
}