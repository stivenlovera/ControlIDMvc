using ControlIDMvc.Dtos.Dia;

namespace ControlIDMvc.Dtos.Horario
{
    public class HorarioCreateDto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<DiaCreateDto> Dias { get; set; }
    }
}