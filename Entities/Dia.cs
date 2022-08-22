using System.ComponentModel.DataAnnotations;

namespace ControlIDMvc.Entities
{
    public class Dia
    {
        [Key]
        [Column("id")]
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime hora_inicio { get; set; }
        public DateTime hora_fin { get; set; }
        [Column("horario_id")]
        public Horario horario { get; set; }
    }
}