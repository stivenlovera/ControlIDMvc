using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DataTableUsuario
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Password { get; set; }
        public int PersonaId { get; set; }
    }
}