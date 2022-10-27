using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class HorarioReglaAcceso
    {
        public int Id { get; set; }

        public int ReglasAccesoId { get; set; }
        public ReglaAcceso ReglasAcceso { get; set; }
        public int HorarioId { get; set; }
        public int ControlIdAccessRulesId { get; set; }
        public int ControlIdTimeZoneId { get; set; }
        public Horario Horario { get; set; }
    }
}