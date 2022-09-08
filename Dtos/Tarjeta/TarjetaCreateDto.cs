using System.ComponentModel.DataAnnotations;
using ControlIDMvc.Entities;

namespace ControlIDMvc.Dtos.Tarjeta
{
    public class TarjetaCreateDto
    {
        public int Area { get; set; }
        public int Codigo { get; set; }
        public int PersonaId { get; set; }
        public string ControlId { get; set; }
        public string Sincronizacion { get; set; }
    }
}