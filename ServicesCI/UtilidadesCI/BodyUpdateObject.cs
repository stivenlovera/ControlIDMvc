using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.UtilidadesCI
{
    public class BodyUpdateObject
    {
       [JsonProperty(PropertyName = "object")]
        public string objeto { get; set; }
        public dynamic values { get; set; }
        public dynamic where { get; set; }
    }
}