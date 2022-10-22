using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.areasDto
{
    public class areaCreateDto
    {
        public string name { get; set; }
    }
    public class responseareaCreateDto
    {
        public List<int> ids { get; set; }
    }
}