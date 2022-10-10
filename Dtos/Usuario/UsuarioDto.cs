using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.RolUsuario;

namespace ControlIDMvc.Dtos.Usuario
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int PersonaId { get; set; }
    }
}