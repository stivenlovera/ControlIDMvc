using System.Runtime.CompilerServices;
namespace ControlIDMvc.Services.QueryControlId
{
    public class LoginControlId
    {
        public LoginControlId()
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