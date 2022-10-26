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
                    password=personaCreateDto.ControlIdPassword,
                    name=personaCreateDto.Nombre,
                    registration=personaCreateDto.ControlIdRegistration,
                    salt=personaCreateDto.ControlIdSalt,
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

        public async Task<ResponseUserUpdate> Update(Persona persona)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "users",
                values = new usersCreateDto
                {
                    begin_time = persona.ControlIdBegin_time,
                    end_time = persona.ControlIdEnd_time,
                    name = persona.ControlIdName,
                    password = persona.ControlIdPassword,
                    registration = persona.ControlIdRegistration,
                    salt = persona.ControlIdSalt
                },
                where = new
                {
                    users = new
                    {
                        id = persona.ControlId
                    }
                }
            };
            var response = await this.RunUpdate(body);
            return response;
        }
        public async Task<ResponseUserShow> MostrarUnoUser(Persona persona)
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "users",
                where = new
                {
                    users = new
                    {
                        id = persona.ControlId
                    }
                }
            };
            var response = await this.RunShow(body);
            return response;
        }
        public async Task<ResponseUserShow> MostrarTodoUser()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "users"
            };
            var response = await this.RunShow(body);
            return response;
        }
        public async Task<ResponseUserDelete> DeleteAll()
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "users"
            };
            var response = await this.RunDelete(body);
            return response;
        }
        public async Task<ResponseUserDelete> Delete(Persona persona)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "users",
                where = new
                {
                    users = new
                    {
                        id = persona.ControlId
                    }
                }
            };
            var response = await this.RunDelete(body);
            return response;
        }
        private async Task<ResponseUserCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseUserCreate responseCreate = new ResponseUserCreate();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (responseAddUsers.estado)
            {
                usersResponseCreateDto responseUser = JsonConvert.DeserializeObject<usersResponseCreateDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.ids = responseUser.ids;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseUserShow> RunShow(BodyShowObject bodyShowAllObject)
        {
            ResponseUserShow responseShow = new ResponseUserShow();

            Response responseUpdate = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (responseUpdate.estado)
            {
                usersResponseDto responseUser = JsonConvert.DeserializeObject<usersResponseDto>(responseUpdate.data);
                responseShow.status = responseUpdate.estado;
                responseShow.usersDto = responseUser.usersDtos;
            }
            else
            {
                responseShow.status = responseUpdate.estado;
            }
            return responseShow;
        }
        private async Task<ResponseUserUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseUserUpdate responseCreate = new ResponseUserUpdate();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (responseAddUsers.estado)
            {
                usersResponseUpdateDto responseUser = JsonConvert.DeserializeObject<usersResponseUpdateDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.changes = responseUser.changes;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseUserDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseUserDelete responsedelete = new ResponseUserDelete();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (responseAddUsers.estado)
            {
                usersResponseDeleteDto responseUser = JsonConvert.DeserializeObject<usersResponseDeleteDto>(responseAddUsers.data);
                responsedelete.status = responseAddUsers.estado;
                responsedelete.changes = responseUser.changes;
            }
            else
            {
                responsedelete.status = responseAddUsers.estado;
            }
            return responsedelete;
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
        public bool status { get; set; }
        public List<usersDto> usersDto { get; set; }
    }
    public class ResponseUserUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
    public class ResponseUserDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}