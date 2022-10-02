using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Dispositivo
{
    public class DispositivoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }
        public string ControlIdIp { get; set; }
        public string ControlIdPublicKey { get; set; }
    }
}