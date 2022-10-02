using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Auth
    {
        public int Id { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string estado { get; set; }
        
    }
}