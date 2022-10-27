using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.areasDto
{

    public class areaUpdateDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class where
    {
        public areas time_Spans { get; set; }
    }
    public class values
    {
        public string name { get; set; }
    }
    public class areas
    {
        public int id { get; set; }
    }
    public class areasResponseUpdateDto
    {
        public int changes { get; set; }
    }
}