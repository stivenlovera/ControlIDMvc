using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Caja
{
    public class CajaCreateDto
    {
        public DateTime Fecha { get; set; }
        public string NumeroRecibo { get; set; }
        public string Concepto { get; set; }
        public string Persona { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
    }
}