global using System.ComponentModel.DataAnnotations.Schema;
namespace ControlIDMvc.Entities
{
    public class Usuario
    {
        public int id { get; set; }
        public string ci { get; set; }
        public string num_tarjeta { get; set; }
        public string contraseña_tarjeta { get; set; } = "";
        public string nombre { get; set; }
        public string image { get; set; } = "usuario-default.webp";
        public DateTime fecha_nac { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public string celular { get; set; }
        public string nota { get; set; }
        public string dirrecion { get; set; }
        public string estado { get; set; } = "activo";
        [ForeignKey("id")]
        public Proyecto proyecto_id { get; set; }
        [ForeignKey("id")]
        public Departamento departamento_id { get; set; }
        [ForeignKey("id")]
        public Grupo grupo_id { get; set; }
        [ForeignKey("id")]
        public virtual Usuario creado_por { get; set; }
    }
}