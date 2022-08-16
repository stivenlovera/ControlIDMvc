namespace ControlIDMvc.Entities
{
    public class Grupo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Proyecto proyecto { get; set; }
        public Persona creado_por { get; set; }
        public string estado { get; set; } = "activo";
    }
}
