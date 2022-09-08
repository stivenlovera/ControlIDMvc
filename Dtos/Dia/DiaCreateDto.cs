namespace ControlIDMvc.Dtos.Dia
{
    public class DiaCreateDto
    {
        public string Nombre { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public int HorarioId { get; set; }
    }
}