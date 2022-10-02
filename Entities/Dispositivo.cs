using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class Dispositivo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public int Puerto { get; set; }
        public string Ip { get; set; }
        public string NumeroSerie { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }
        public string ControlIdIp { get; set; }
        public string ControlIdPublicKey { get; set; }
        public List<Portal> Portals { get; set; }
        public int ProyectoId { get; set; }
        public Proyecto proyecto { get; set; }
    }
}