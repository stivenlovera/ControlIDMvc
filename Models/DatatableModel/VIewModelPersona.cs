namespace ControlIDMvc.Models.DatatableModel
{
    public class PersonaModel
    {
        public int id { get; set; }
        public string ci { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_nac { get; set; }
        public string email { get; set; }
        public string celular { get; set; }
        public string observaciones { get; set; }
        public string usuario { get; set; }
    }
}