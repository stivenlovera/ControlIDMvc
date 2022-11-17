using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.registroRostroDto
{
    public class RegistroRostroDto
    {
        public List<int> user_ids { get; set; }
    }
    public class responseRegistroRostroDto
    {
        public int id { get; set; }
        public long timestamp { get; set; }
        public string image { get; set; }
    }
}