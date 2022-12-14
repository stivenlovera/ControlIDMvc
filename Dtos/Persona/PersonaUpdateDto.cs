using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControlIDMvc.Dtos.Persona
{
    public class PersonaUpdateDto
    {
        [Required(ErrorMessage = "CI requerido")]
        public string Ci { get; set; }
        [Required(ErrorMessage = "EL Nombre requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "EL Apellido requerido")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "EL Fecha Nacimiento requerido")]
        public DateTime Fecha_nac { get; set; }
        [Required(ErrorMessage = "EL Email requerido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "EL Celular requerido")]
        public string Celular { get; set; }
        public string Dirrecion { get; set; }
        public string Observaciones { get; set; }
        public string Usuario { get; set; }
        public int ControlId { get; set; }
        public string ControlIdPassword { get; set; }
        public string ControlIdName { get; set; }
        public string ControlIdSalt { get; set; }
        public string ControlIdRegistration { get; set; }
        public List<string> Area { get; set; }
        public List<string> Codigo { get; set; }
        public DateTime ControlIdBegin_time { get; set; }
        public DateTime ControlIdEnd_time { get; set; }
    }
}