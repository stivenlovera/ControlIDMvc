global using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ControlIDMvc.Entities
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Ci { get; set; }
        public string NumTarjeta { get; set; }
        public string ContraseñaTarjeta { get; set; } = "";
        public string Nombre { get; set; }
        public string Image { get; set; } = "usuario-default.webp";
        public DateTime Fecha_nac { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string nota { get; set; }
        public string dirrecion { get; set; }
        public string Estado { get; set; } = "activo";
        public Proyecto proyecto_id { get; set; }
        public Departamento departamento_id { get; set; }
        public Grupo grupo_id { get; set; }
        public virtual Usuario creado_por { get; set; }
    }
}