using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.PersonaReglasAcceso
{
    public class PersonaAccesoReglasCreateDto
    {
        public int PersonaId { get; set; }
        public int ReglaAccesoId { get; set; }
        public int ControlIdUserId { get; set; }
        public int ControlIdAccessRulesId { get; set; }
    }
}