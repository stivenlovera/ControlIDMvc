using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class MetodoPago
    {
        public int Id { get; set; }
        public string NombreMetodo { get; set; }
        public string PlanCuenta { get; set; }
        public string PlanId { get; set; }
        public string PlanCuentaPadre { get; set; }
        public string PlanaPadreId { get; set; }
        public List<Inscripcion> inscripcion { get; set; }
    }
}