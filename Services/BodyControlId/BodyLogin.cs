using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ControlIDMvc.Services.BodyControlId
{
    public class BodyLogin
    {
        [JsonProperty(PropertyName = "login")]
        public string login { get; set; }
        public string password { get; set; }
    }
}