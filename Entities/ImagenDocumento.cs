using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class ImagenDocumento
    {
        public int Id { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Caption { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
    }
}