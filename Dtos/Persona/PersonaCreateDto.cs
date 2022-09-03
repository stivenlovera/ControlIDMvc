using ControlIDMvc.Dtos.Tarjeta;
using Newtonsoft.Json;

namespace ControlIDMvc.Dtos
{
    public class PersonaCreateDto
    {
        public string Ci { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Fecha_nac { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public string Dirrecion { get; set; }
        public string Observaciones { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string Sincronizacion { get; set; }
        [JsonProperty(PropertyName = "cards[]")]
        public List<int> Cards { get; set; }
    }
}