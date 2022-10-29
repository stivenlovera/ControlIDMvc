using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.portalsDto
{
    public class portalsUpdateDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class where
    {
        public portals portals { get; set; }
    }
    public class values
    {
        public string name { get; set; }
        public int area_from_id { get; set; }
        public int area_to_id { get; set; }
    }
    public class portals
    {
        public int id { get; set; }
    }
    public class portalsResposeUpdateDto
    {
        public int changes { get; set; }
    }
}