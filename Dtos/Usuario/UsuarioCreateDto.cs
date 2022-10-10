using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Usuario
{
    public class UsuarioCreateDto
    {
        public string User { get; set; }
        public string Password { get; set; }
        public int PersonaId { get; set; }
    }
    public class UserRolesCreateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "CI es requerido")]
        public string Ci { get; set; }
        [Required(ErrorMessage = "Nombres es requerido")]
        public string nombres { get; set; }
        [Required(ErrorMessage = "Apellido es requerido")]
        public string apellidos { get; set; }
        [Required(ErrorMessage = "Debe selecionar almenos un rol")]
        public List<string> RolIds { get; set; }
        [Required(ErrorMessage = "Debe selecionar un usuario")]
        public string User { get; set; }
        [Required(ErrorMessage = "Debe selecionar una contraseña")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Error persona")]
        public int PersonaId { get; set; }
    }
}