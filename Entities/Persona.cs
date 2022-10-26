using System.Security.Cryptography.X509Certificates;
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
        public string ControlIdPassword { get; set; }
        public string ControlIdName { get; set; }
        public string ControlIdSalt { get; set; }
        public string ControlIdRegistration { get; set; }
        public int ControlId { get; set; }
        public long ControlIdBegin_time { get; set; }
        public long ControlIdEnd_time { get; set; }
        public List<Tarjeta> card { get; set; }
        public List<ImagenDocumento> documentos { get; set; }
        public List<ImagenPerfil> perfiles { get; set; }
        public List<Inscripcion> Inscripciones { get; set; }
        public Usuario Usuario { get; set; }
    }
}