using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.actionsDto
{
    public class actionsCreateDto
    {
        public string name { get; set; }
        public string action { get; set; }
        public string parameters { get; set; }
        public int run_at { get; set; }
    }
    public class actionsCreateResponseDto
    {
        public List<int> ids { get; set; }
    }
}