namespace ControlIDMvc.Dtos.Persona
{
    public class PersonaDto
    {
        public int Id { get; set; }
        public string Ci { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_nac { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Dirrecion { get; set; }
        public string Observaciones { get; set; }
        public string Usuario { get; set; }
        public string ControlIdPassword { get; set; }
        public string ControlIdName { get; set; }
        public string ControlIdSalt { get; set; }
        public string ControlIdRegistration { get; set; }
        public string Sincronizacion { get; set; }
        public string ControlId { get; set; }
        public List<string> Area { get; set; }
        public List<string> Codigo { get; set; }
        public int ControlIdBegin_time { get; set; }
        public int ControlIdEnd_time { get; set; }
    }
}