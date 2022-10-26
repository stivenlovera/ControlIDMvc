using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.actionsDto
{
    public class actionsUpdateDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class where
    {
        public actions actions { get; set; }
    }
    public class values
    {
        public string name { get; set; }
        public string action { get; set; }
        public string parameters { get; set; }
        public int run_at { get; set; }
    }
    public class actions
    {
        public int id { get; set; }
    }
    public class actionsResponseUpdateDto
    {
        public int changes { get; set; }
    }
}