namespace ControlIDMvc.Models.DatatableModel
{
    public class Persona
    {
        public int id { get; set; }
        public string ci { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_nac { get; set; }
        public string email { get; set; }
        public string celular { get; set; }
        public string dirrecion { get; set; }
        public string observaciones { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public string image { get; set; }
        public string image_documento { get; set; }
        public Persona creado_por { get; set; }
    }
}