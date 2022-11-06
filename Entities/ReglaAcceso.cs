using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class ReglaAcceso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }
        public int ControlIdType { get; set; }
        public int ControlIdPriority { get; set; }
        public string Descripcion { get; set; }
        public List<PortalReglaAcceso> PortalReglaAccesos { get; set; }
        public List<AreaReglaAcceso> AreaSReglaAccesos { get; set; }
        public List<PersonaReglasAcceso> PersonaReglasAcceso { get; set; }
         public List<HorarioReglaAcceso> HorarioReglasAcceso { get; set; }
    }
}