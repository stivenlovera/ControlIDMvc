using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos.Rol;

namespace ControlIDMvc.Dtos.RolUsuario
{
    public class RolUsuarioDto
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int UsuarioId { get; set; }
    }
}