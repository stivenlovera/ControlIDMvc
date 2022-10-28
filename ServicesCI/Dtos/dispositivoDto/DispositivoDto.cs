using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.dispositivoDto
{
    public class DispositivoDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string ip { get; set; }
        public string public_key { get; set; }
    }
    public class DispositivoResposeDto
    {
        public List<DispositivoDto> devices { get; set; }
    }
}