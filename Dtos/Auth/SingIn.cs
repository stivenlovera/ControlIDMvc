using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Auth
{
    public class SingIn
    {
        public string usuario { get; set; }
        public bool recuerdame { get; set; }
        public string password { get; set; }
    }
}