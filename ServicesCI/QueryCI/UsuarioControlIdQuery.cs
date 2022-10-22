using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using ControlIDMvc.ServicesCI.Dtos.usersDto;
using Newtonsoft.Json;
using ControlIDMvc.Dtos;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class UsuarioControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public UsuarioControlIdQuery(HttpClientService httpClientService)
        {
            this._httpClientService = httpClientService;
            this._ApiRutas = new ApiRutas();

        }

        public void Params(int port, string controlador, string user, string password, string session)
        {
            this.port = port;
            this.controlador = controlador;
            this.user = user;
            this.password = password;
            this.session = session;
        }

        public async Task<ResponseUserCreate> CreateOneUser(Persona personaCreateDto)
        {
            var usuario = new List<usersCreateDto>(){
                new usersCreateDto{
                    begin_time=2,
                    end_time=2,
                    password="",
                    name=personaCreateDto.Nombre,
                    registration="",
                    salt="",

                }
            };
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "users",
                values = usuario
            };
            var response = await this.RunCreate(body);
            return response;
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
        private async Task<ResponseUserCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseUserCreate responseCreate = new ResponseUserCreate();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (responseAddUsers.estado)
            {
                usersResponseDto responseUser = JsonConvert.DeserializeObject<usersResponseDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.ids = responseUser.ids;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
    }
    /*clases de ayuda*/
    public class ResponseUserCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseUserShow
    {
        public List<int> ids { get; set; }
    }
}