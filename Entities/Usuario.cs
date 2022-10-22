using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public List<RolUsuario> RolUsuarios { get; set; }
        public List<Egreso> Egresos { get; set; }
    }
}