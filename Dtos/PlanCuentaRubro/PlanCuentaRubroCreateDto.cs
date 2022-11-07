using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.PlanCuentaRubro
{
    public class PlanCuentaRubroCreateDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Nivel { get; set; }
        public int PlanCuentaGrupoId { get; set; }
    }
}