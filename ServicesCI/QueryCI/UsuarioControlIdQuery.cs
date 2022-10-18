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
        public string controlador = "192.168.88.129";
        public string user = "admin";
        public string password = "admin";
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public UsuarioControlIdQuery(HttpClientService httpClientService)
        {
            this._httpClientService = httpClientService;
            this._ApiRutas = new ApiRutas();
        }

        public string ApiUrl { get; set; }

        public async Task<ResponseCreate> CreateOneUser(PersonaCreateDto personaCreateDto)
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
        private async Task<ResponseCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseCreate responseCreate = new ResponseCreate();
            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this._ApiRutas.ApiUrlCreate, bodyCreateObject);
            if (responseAddUsers.estado)
            {
                usersResponseDto responseUser = JsonConvert.DeserializeObject<usersResponseDto>(responseAddUsers.data);
                responseCreate.status = false;
                responseCreate.ids = responseUser.ids;
            }
            else
            {
                responseCreate.status = false;
            }
            return responseCreate;
        }
    }
    /*clases de ayuda*/
    public class ResponseCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseShow
    {
        public List<int> ids { get; set; }
    }
}