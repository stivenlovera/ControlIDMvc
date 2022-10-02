using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.HorarioReglaAcceso
{
    public class HorarioReglaAccesoDto
    {
        public int Id { get; set; }
        public int ReglasAccesoId { get; set; }
        public int ControlIdAccessRulesId { get; set; }
        public int HorarioId { get; set; }
        public int ControlIdTimeZoneId { get; set; }
    }
}