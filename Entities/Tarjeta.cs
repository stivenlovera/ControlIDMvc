namespace ControlIDMvc.Entities
{
    public class Tarjeta
    {
        public int Id { get; set; }
        public int area { get; set; }
        public int codigo { get; set; }
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        public int ControlId { get; set; }
        public long ControlIdValue { get; set; }
        public int ControlIdUserId { get; set; }
        public string ControlIdsecret { get; set; }
    }
}