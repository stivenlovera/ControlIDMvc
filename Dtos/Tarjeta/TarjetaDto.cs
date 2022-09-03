using ControlIDMvc.Entities;

namespace ControlIDMvc.Dtos.Tarjeta
{
    public class TarjetaDto
    {
        public int tarjeta { get; set; }
        public int usuario_id { get; set; }
        public Persona usuario { get; set; }
        public string Sincronizacion { get; set; }
    }
}