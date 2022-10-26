using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.actionsDto
{
    public class actionsDeleteDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class actionsResponseDeleteDto
    {
        public int changes { get; set; }
    }
}