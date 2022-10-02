using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using ControlIDMvc.ServicesCI.Dtos.usersDto;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class UsuarioControlIdQuery
    {
        public string ApiUrl { get; set; }
        public BodyCreateObject CreateUser(List<usersCreateDto> personas)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "users",
                values = personas
            };
            return body;
        }
        public BodyUpdateObject UpdateUser(int id, usersCreateDto users)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "users",
                values = users,
                where = new
                {
                    users = new
                    {
                        id = id
                    }
                }
            };
            return body;
        }
        public BodyUpdateObject MostrarUnoUser(int id)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "users",
                where = new
                {
                    users = new
                    {
                        id = id
                    }
                }
            };
            return body;
        }
        public BodyUpdateObject MostrarTodoUser()
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "users"
            };
            return body;
        }
        public BodyUpdateObject DeleteTodoUser()
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "users"
            };
            return body;
        }
        public BodyUpdateObject DeleteUnoUser(int id)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "users",
                where = new
                {
                    users = new
                    {
                        id = id
                    }
                }
            };
            return body;
        }
    }

}