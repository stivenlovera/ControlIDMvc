using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.areasDto
{
    public class areaDto
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class areaResponseDto
    {
        public List<areaDto> areas { get; set; }
    }
}