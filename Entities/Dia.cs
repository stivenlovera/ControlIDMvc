using System.ComponentModel.DataAnnotations;

namespace ControlIDMvc.Entities
{
    public class Dia
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime hora_inicio { get; set; }
        public DateTime hora_fin { get; set; }
        [ForeignKey("id")]
        [Column("horarioid")]
        public int horarioid { get; set; }
        public Horario horario { get; set; }
    }
}