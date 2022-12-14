using System.ComponentModel.DataAnnotations;

namespace ControlIDMvc.Entities
{
    public class Horario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int ControlId { get; set; }
        public string ControlIdName { get; set; }
        public List<Dia> Dias { get; set; }
        public List<HorarioReglaAcceso> HorarioReglaAccesos { get; set; }
    }
}