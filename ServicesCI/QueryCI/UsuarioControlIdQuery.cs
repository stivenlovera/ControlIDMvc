using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using ControlIDMvc.ServicesCI.Dtos.usersDto;
using ControlIDMvc.Dtos;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class UsuarioControlIdQuery
    {
        public string ApiUrl { get; set; }
        public UsuarioControlIdQuery()
        {
        }
        public BodyCreateObject CreateUser(List<PersonaCreateDto> personas)
        {
            usersCreateDto user = new usersCreateDto();
            foreach (var persona in personas)
            {
                user.name = persona.Nombre;
                user.password = "";
                user.registration = "";
                user.salt = "";
            }
            List<usersCreateDto> users = new List<usersCreateDto>();
            users.Add(user);

            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "users",
                values = users
            };
            return body;
        }
        
    }

}