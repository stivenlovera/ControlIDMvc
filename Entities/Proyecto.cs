namespace ControlIDMvc.Entities
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Dirrecion { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime Fecha_fin { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; } = "creado";
        public List<Dispositivo> Dispositivos { get; set; }
    }
   
}