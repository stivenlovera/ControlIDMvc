namespace ControlIDMvc.Entities
{
    public class Departamento
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int proyecto_id { get; set; }
        public int creado_por { get; set; }
        public string estado { get; set; } = "activo";
    }
}