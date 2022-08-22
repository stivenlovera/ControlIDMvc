using System.ComponentModel.DataAnnotations;

namespace ControlIDMvc.Entities
{
    public class Horario
    {
        [Key]
        [Required]
        [Column("id")]
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        [ForeignKey("horario_id")]
        public List<Dia> dias { get; set; }
    }
}