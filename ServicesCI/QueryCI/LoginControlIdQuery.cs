using System.Runtime.CompilerServices;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
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
        }
    }
}