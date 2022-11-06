using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.Dtos.user_access_rules;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class UsuarioRulesAccessControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public UsuarioRulesAccessControlIdQuery(HttpClientService httpClientService)
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

        public BodyCreateObject CreateUserReglaAcceso(List<userAccessRulesCreateDto> reglaAcceso)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "user_access_rules",
                values = reglaAcceso
            };
            return body;
        }
        public async Task<ResponseUserAccessRolesDelete> DeleteReglaAccesoId(int access_rule_id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "user_access_rules",
                where = new
                {
                    user_access_rules = new
                    {
                        access_rule_id = access_rule_id
                    }
                }
            };
            return await  this.RunDelete(body);
        }
        public BodyDeleteObject DeleteUserId(int user_id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "user_access_rules",
                where = new
                {
                    user_access_rules = new
                    {
                        user_id = user_id
                    }
                }
            };
            return body;
        }
        public BodyDeleteObject DeleteUser(int access_rule_id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "user_access_rules",
                where = new
                {
                    user_access_rules = new
                    {
                        access_rule_id = access_rule_id
                    }
                }
            };
            return body;
        }

        public async Task<ResponsUserAccessRolesrCreate> CreateAll(List<PersonaReglasAcceso> personaCreateDto)
        {
            var usuarios = new List<userAccessRulesCreateDto>();
            foreach (var personaCreate in personaCreateDto)
            {
                usuarios.Add(new userAccessRulesCreateDto{
                    access_rule_id=personaCreate.ControlIdUserId,
                    user_id=personaCreate.ControlIdAccessRulesId
                });
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "user_access_rules",
                values = usuarios
            };
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseUserAccessRolesDelete> Delete(PersonaReglasAcceso personaReglasAcceso)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "user_access_rules",
                where = new
                {
                    user_access_rules = new
                    {
                        access_rule_id = personaReglasAcceso.ReglaAccesoId
                    }
                }
            };
            var response = await this.RunDelete(body);
            return response;
        }

        private async Task<ResponsUserAccessRolesrCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponsUserAccessRolesrCreate responseCreate = new ResponsUserAccessRolesrCreate();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (apiResponse.estado)
            {
                responseUserAccessRulesCreateDto response = JsonConvert.DeserializeObject<responseUserAccessRulesCreateDto>(apiResponse.data);
                responseCreate.status = apiResponse.estado;
                responseCreate.ids = response.ids;
            }
            else
            {
                responseCreate.status = apiResponse.estado;
            }
            return responseCreate;
        }

        private async Task<ResponseUserAccessRolesDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseUserAccessRolesDelete responsedelete = new ResponseUserAccessRolesDelete();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (apiResponse.estado)
            {
                userAccessRuleResponseDeleteDto response = JsonConvert.DeserializeObject<userAccessRuleResponseDeleteDto>(apiResponse.data);
                responsedelete.status = apiResponse.estado;
                responsedelete.changes = response.changes;
            }
            else
            {
                responsedelete.status = apiResponse.estado;
            }
            return responsedelete;
        }
    }
    /*clases de ayuda*/
    public class ResponsUserAccessRolesrCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    /* public class ResponseUserRolesShow
    {
        public bool status { get; set; }
        public List<usersDto> usersDto { get; set; }
    } */
    public class ResponseUserAccessRolesUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
    public class ResponseUserAccessRolesDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}