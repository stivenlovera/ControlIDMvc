using System.ComponentModel.DataAnnotations;

namespace ControlIDMvc.Entities
{
    public class Dia
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int HorarioId { get; set; }
        public Horario Horario { get; set; }
    }
}