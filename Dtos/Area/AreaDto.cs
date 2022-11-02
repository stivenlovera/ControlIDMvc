using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.Area
{
    public class AreaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }

        public List<string> PuertasSelecionadasId { get; set; }
        public List<string> PuertasSelecionadasNombre { get; set; }//puerta entrada
        public List<string> PuertasSelecionadasAreaEntradaNombre { get; set; }
        public List<string> PuertasSelecionadasAreaEntrada { get; set; }
        public List<string> PuertasSelecionadasAreaSalida { get; set; } //puerta salida
        public List<string> PuertasSelecionadasAreaSalidaNombre { get; set; }

        public List<string> PuertasDisponibleId { get; set; }
        public List<string> PuertasDisponibleNombre { get; set; }
        public List<string> PuertasDisponibleAreaEntrada { get; set; }
        public List<string> PuertasDisponibleAreaEntradaNombre { get; set; }
        public List<string> PuertasDisponibleAreaSalida { get; set; }
        public List<string> PuertasDisponibleAreaSalidaNombre { get; set; }
    }
}