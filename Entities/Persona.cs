using System.ComponentModel.DataAnnotations;
namespace ControlIDMvc.Entities
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }
        public string Ci { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_nac { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Dirrecion { get; set; }
        public string Observaciones { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Sincronizacion { get; set; }
        public string ControlId { get; set; }
        public List<Tarjeta> card { get; set; }
        public List<ImagenDocumento> documentos { get; set; }
        public List<ImagenPerfil> perfiles { get; set; }
    }
}