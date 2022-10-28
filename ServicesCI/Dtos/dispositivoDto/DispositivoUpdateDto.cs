using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.dispositivoDto
{
    public class DispositivoUpdateDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class where
    {
        public devices devices { get; set; }
    }
    public class values
    {
        public string ip { get; set; }
    }
    public class devices
    {
        public int id { get; set; }
    }
    public class dispositivoResposeUpdateDto
    {
        public int changes { get; set; }
    }
}