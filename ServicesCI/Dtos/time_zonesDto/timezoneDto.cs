using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.time_zonesDto
{
    public class timezoneDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }
     public class time_zonesResponseDto
    {
       public List<timezoneDto> timezoneDtos { get; set; }
    }
}