using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Login
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Usuario requerido")]
        public string User { get; set; }    
        public bool recuerdame { get; set; }
         [Required(ErrorMessage = "Password requerido")]
        public string Password { get; set; }
    }
}