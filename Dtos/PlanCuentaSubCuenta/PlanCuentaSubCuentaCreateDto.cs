using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.PlanCuentaSubCuenta
{
    public class PlanCuentaSubCuentaCreateDto
    {
        [Display(Name = "Codigo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El {0} es obligatorio")]
        public string Codigo { get; set; }
        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El {0} es obligatorio")]
        public string Nombre { get; set; }
        public string Nivel { get; set; }
        public int Moneda { get; set; }
        public decimal Valor { get; set; }
        public string codigoPadreCompuesta { get; set; }
        public int PlanCuentaCompuestaId { get; set; }
    }
}