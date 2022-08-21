namespace ControlIDMvc.Models.ModelForm
{
    public class HorarioForm
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public List<string> dia { get; set; }
        public List<DateTime> hora_inicio { get; set; }
        public List<DateTime> hora_fin { get; set; }
    }
}