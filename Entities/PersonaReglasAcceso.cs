using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class PersonaReglasAcceso
    {
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public int ReglaAccesoId { get; set; }
        public int ControlIdUserId { get; set; }
        public int ControlIdAccessRulesId { get; set; }
        public ReglaAcceso ReglaAcceso { get; set; }
    }
}