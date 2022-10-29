using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Area
{
    public class AreaCreateDto
    {
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }
        public List<string> PuertasSelecionadas { get; set; }
        public List<string> PuertasDisponibles { get; set; }
    }
}