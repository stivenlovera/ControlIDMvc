using System.ComponentModel.DataAnnotations;
using ControlIDMvc.Entities;

namespace ControlIDMvc.Dtos.Tarjeta
{
    public class TarjetaCreateDto
    {
        public int Area { get; set; }
        public int Codigo { get; set; }
        public int PersonaId { get; set; }
        public int ControlId { get; set; }
        public long ControlIdValue { get; set; }
        public int ControlIdUserId { get; set; }
        public string ControlIdsecret { get; set; }
    }
}