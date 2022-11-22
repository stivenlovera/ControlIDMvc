using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.PlanCuentaCompuesta
{
    public class PlanCuentaCompuestaCreateDto
    {
        [Display(Name = "Codigo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El {0} es obligatorio")]
        public string Codigo { get; set; }
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El {0} es obligatorio")]
        public string Nombre { get; set; }
        public string Nivel { get; set; }
        public string codigoPadreTitulo { get; set; }
        public int PlanCuentaTituloId { get; set; }
    }
}