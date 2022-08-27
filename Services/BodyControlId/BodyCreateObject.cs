using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Services.ControlId;
using Newtonsoft.Json;

namespace ControlIDMvc.Services.BodyControlId
{
    public class BodyCreateObject
    {
        [JsonProperty(PropertyName = "object")]
        public string objeto { get; set; }
        public dynamic values { get; set; }
        //public dynamic where { get; set; }
    }
    


}