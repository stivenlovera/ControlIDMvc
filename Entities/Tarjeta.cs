namespace ControlIDMvc.Entities
{
    public class Tarjeta
    {
        public int Id { get; set; }
        public int tarjeta { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public string Sincronizacion { get; set; }
    }
}