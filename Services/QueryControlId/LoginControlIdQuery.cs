using System.Runtime.CompilerServices;
using ControlIDMvc.Services.BodyControlId;
using Newtonsoft.Json;

namespace ControlIDMvc.Services.QueryControlId
{
    public class LoginControlIdQuery
    {
        public string ApiUrl { get; set; }
        public LoginControlIdQuery()
        {
        }

        public BodyLogin Login(string usuario, string contraseña)
        {
            BodyLogin body = new BodyLogin
            {
                login = usuario,
                password = contraseña
            };
            return body;
            /*  return new
             {
                 login = usuario,
                 password = contraseña
             }; */
            /* JObject response = new JObject{
                new JProperty("object",usuario),
                new JProperty("values",contraseña)
                
            };
            return response.First; */
        }
    }
}