using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.UtilidadesCI
{
    public class BodyCreateObject
    {
        [JsonProperty(PropertyName = "object")]
        public string objeto { get; set; }
        public dynamic values { get; set; }
        //public dynamic where { get; set; }
    }

}