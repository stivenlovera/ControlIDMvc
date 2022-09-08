namespace ControlIDMvc.Entities
{
    public class Tarjeta
    {
        public int Id { get; set; }
        public int area { get; set; }
        public int codigo { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public string Sincronizacion { get; set; }
    }
}