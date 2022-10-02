using System.Security.AccessControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Portal
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ControlId { get; set; }
        public string ControlIdName { get; set; }
        public int ControlIdAreaFromId { get; set; }
        public int ControlIdAreaToId { get; set; }
        public List<AccionPortal> AccionePortal { get; set; }
        public List<PortalReglaAcceso> PortalReglaAcceso { get; set; }
        public int DispositivoId { get; set; }
        public Dispositivo Dispositivo { get; set; }
    }
}