using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DataTableRolesUsuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Roles { get; set; }
        public string Usuario { get; set; }
        public int UsuarioId { get; set; }
    }
}