using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Dispositivo
{
    public class ProbarConexionDto
    {
        [Required(ErrorMessage = "Usuario es requerido")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Password es requerido")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Ip es requerido")]
        public string Ip { get; set; }
        [Required(ErrorMessage = "Puerto es requerido")]
        public int Puerto { get; set; }
    }
}