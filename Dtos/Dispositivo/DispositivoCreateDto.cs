using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Dispositivo
{
    public class DispositivoCreateDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Modelo es requerido")]
        public string Modelo { get; set; }
        [Required(ErrorMessage = "Ip es requerido")]
        public string Ip { get; set; }
        [Required(ErrorMessage = "Puerto es requerido")]
        [Range(2, Int32.MaxValue, ErrorMessage = "Debe ser un numero mayor o igual a 2 digitos")]
        public int Puerto { get; set; }
        public string NumeroSerie { get; set; }
        public bool? probarConexion { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }
        public string ControlIdIp { get; set; }
        public string ControlIdPublicKey { get; set; }
    }
}