using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.access_rulesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class AccessRulesControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public AccessRulesControlIdQuery(HttpClientService httpClientService)
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
        public async Task<ResponseAccesoRulesCreate> CreateAccessRules(List<accessRulesCreateDto> accessRules)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "access_rules",
                values = accessRules
            };
            var response = await this.RunCreate(body);
            return response;
        }
        public BodyShowObject MostrarAccessRules()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "access_rules"
            };
            return body;
        }
        public BodyShowObject MostrarOneAccessRules(int id)
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "access_rules",
                where = new
                {
                    access_rules = new
                    {
                        id = id
                    }
                },
            };
            return body;
        }
        public BodyDeleteObject DeleteAccessRules(int id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "access_rules",
                where = new
                {
                    access_rules = new
                    {
                        id = id
                    }
                },
            };
            return body;
        }
        private async Task<ResponseAccesoRulesCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseAccesoRulesCreate responseCreate = new ResponseAccesoRulesCreate();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (responseAddUsers.estado)
            {
                responseApiAccessRulesCreateDto responseUser = JsonConvert.DeserializeObject<responseApiAccessRulesCreateDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.ids = responseUser.ids;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseAccesoRulesShow> RunShow(BodyShowAllObject bodyShowAllObject)
        {
            ResponseAccesoRulesShow responseCreate = new ResponseAccesoRulesShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (responseAddUsers.estado)
            {
                responseApiAccessRulesDto responseUser = JsonConvert.DeserializeObject<responseApiAccessRulesDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.accessRulesDtos = responseUser.accessRulesDtos;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseAccesoRulesShow> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseAccesoRulesShow responseCreate = new ResponseAccesoRulesShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (responseAddUsers.estado)
            {
                responseApiAccessRulesDto responseUser = JsonConvert.DeserializeObject<responseApiAccessRulesDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.accessRulesDtos = responseUser.accessRulesDtos;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseAccesoRulesShow> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseAccesoRulesShow responseCreate = new ResponseAccesoRulesShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (responseAddUsers.estado)
            {
                responseApiAccessRulesDto responseUser = JsonConvert.DeserializeObject<responseApiAccessRulesDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.accessRulesDtos = responseUser.accessRulesDtos;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
    }
    /*clases de ayuda*/
    public class ResponseAccesoRulesCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseAccesoRulesShow
    {
        public bool status { get; set; }
        public List<accessRulesDto> accessRulesDtos { get; set; }
    }
     public class ResponseAccesoRulesUpdate
    {
        public bool status { get; set; }
        public string changes { get; set; }
    }
     public class ResponseAccesoRulesDelete
    {
        public bool status { get; set; }
        public string changes { get; set; }
    }
}