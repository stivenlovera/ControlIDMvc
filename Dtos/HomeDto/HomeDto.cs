using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.HomeDto
{
    public class HomeDto
    {
        public List<Totales> Totales { get; set; }
    }
    public class Totales
    {
        public decimal total { get; set; }
        public string PlanId { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }="Resumen de esta semana";
    }
}