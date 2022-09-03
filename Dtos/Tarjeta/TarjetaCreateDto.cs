using ControlIDMvc.Entities;

namespace ControlIDMvc.Dtos.Tarjeta
{
    public class TarjetaCreateDto
    {
        public int tarjeta { get; set; }
        public int usuario_id { get; set; }
        public string Sincronizacion { get; set; }
    }
}