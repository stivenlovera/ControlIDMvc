namespace ControlIDMvc.Entities
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Proyecto Proyecto { get; set; }
        public Persona Creado_por { get; set; }
        public string Estado { get; set; } = "activo";
    }
}
