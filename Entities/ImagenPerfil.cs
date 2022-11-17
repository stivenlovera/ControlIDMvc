using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class ImagenPerfil
    {
        public int Id { get; set; }
        public long Size { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Caption { get; set; }
        public int PersonaId { get; set; }
        public int ControlUserId { get; set; }
        public long ControlIdTimestamp { get; set; }
        public string ControlIdImage { get; set; }
        public string base64 { get; set; }
        public Persona Persona { get; set; }
    }
}