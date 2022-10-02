using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class AreaReglaAcceso
    {
        public int Id { get; set; }
        public int ControlIdAreaId { get; set; }
        public Area Area { get; set; }
        public int ControlidReglaAccesoId { get; set; }
        public ReglaAcceso ReglaAcceso { get; set; }
    }
}