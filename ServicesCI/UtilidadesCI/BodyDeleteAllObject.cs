using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.UtilidadesCI
{
    public class BodyDeleteAllObject
    {

        [JsonProperty(PropertyName = "object")]
        public string objeto { get; set; }

    }
}