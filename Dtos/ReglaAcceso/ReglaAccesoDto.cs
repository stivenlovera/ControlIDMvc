using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.ReglaAcceso
{
    public class ReglaAccesoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ControlId { get; set; }
        public string ControlIdName { get; set; }
        public int ControlIdType { get; set; }
        public int ControlIdPriority { get; set; }
        //ocupadas
        public List<ControlIDMvc.Entities.Persona> personasOcupadas { get; set; }
        public List<ControlIDMvc.Entities.Horario> horariosOcupadas { get; set; }
        public List<ControlIDMvc.Entities.Area> areasOcupadas { get; set; }
          //disponibles
        public List<ControlIDMvc.Entities.Persona> personasDisponibles { get; set; }
        public List<ControlIDMvc.Entities.Horario> horariosDisponibles { get; set; }
        public List<ControlIDMvc.Entities.Area> areasDisponibles { get; set; }
    }
}