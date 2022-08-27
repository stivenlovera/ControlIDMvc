using System.Runtime.CompilerServices;
namespace ControlIDMvc.Services.QueryControlId
{
    public class LoginControlIdQuery
    {
        public string ApiUrl { get; set; }
        public LoginControlIdQuery()
        {
        }

        public object Login(string usuario, string contraseña)
        {
            return new
            {
                login = usuario,
                password = contraseña
            };
        }
    }
}