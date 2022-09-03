namespace ControlIDMvc.Entities
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Creado_por { get; set; }
        public string Estado { get; set; } = "activo";
        public int ProyectoId { get; set; }
    }
}