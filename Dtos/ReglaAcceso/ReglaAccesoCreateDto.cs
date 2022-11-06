using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.ReglaAcceso
{
    public class ReglaAccesoCreateDto
    {
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ControlId { get; set; }
        public string ControlIdName { get; set; }
        public int ControlIdType { get; set; }
        public int ControlIdPriority { get; set; }

        public List<int> PersonasSelecionadas { get; set; }
        public List<int> HorarioSelecionados { get; set; }
        public List<int> AreaSelecionadas { get; set; }
    }
}