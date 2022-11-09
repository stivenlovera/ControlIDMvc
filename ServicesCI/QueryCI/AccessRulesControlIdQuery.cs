using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
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
        public async Task<ResponseAccesoRulesCreate> CreateAccessRules(List<ReglaAcceso> reglaAccesos)
        {
            var accesosRules= new List<accessRulesCreateDto>();
            foreach (var reglaAcceso in reglaAccesos)
            {
                accesosRules.Add(new accessRulesCreateDto{
                    name=reglaAcceso.Nombre,
                    priority=0,
                    type=0
                });
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "access_rules",
                values = accesosRules
            };
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseAccesoRulesShow> showAll()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "access_rules"
            };
            var response = await this.RunShowAll(body);
            return response;
        }
        public async Task<ResponseAccesoRulesUpdate> Update(ReglaAcceso reglaAcceso)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "access_rules",
                values = new accessRulesCreateDto
                {
                    name = reglaAcceso.Nombre,
                    priority = reglaAcceso.ControlIdType,
                    type = reglaAcceso.ControlIdType
                },
                where = new
                {
                    access_rules = new
                    {
                        id = reglaAcceso.ControlId
                    }
                }
            };
            var response = await this.RunUpdate(body);
            return response;
        }
        public async Task<ResponseAccesoRulesShow> ShowOne(ReglaAcceso reglaAcceso)
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "access_rules",
                where = new
                {
                    access_rules = new
                    {
                        id = reglaAcceso.ControlId
                    }
                },
            };
            var response = await this.RunShow(body);
            return response;
        }
        public async Task<ResponseAccesoRulesDelete> Delete(ReglaAcceso reglaAcceso)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "access_rules",
                where = new
                {
                    access_rules = new
                    {
                        id = reglaAcceso.ControlId
                    }
                },
            };
            var response = await this.RunDelete(body);
            return response;
        }
        private async Task<ResponseAccesoRulesCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseAccesoRulesCreate apiResponseCreate = new ResponseAccesoRulesCreate();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (responseAddUsers.estado)
            {
                AccessRulesCreateResponseDto responseUser = JsonConvert.DeserializeObject<AccessRulesCreateResponseDto>(responseAddUsers.data);
                apiResponseCreate.status = responseAddUsers.estado;
                apiResponseCreate.ids = responseUser.ids;
            }
            else
            {
                apiResponseCreate.status = responseAddUsers.estado;
            }
            return apiResponseCreate;
        }
        private async Task<ResponseAccesoRulesShow> RunShow(BodyShowObject bodyShowObject)
        {
            ResponseAccesoRulesShow apiResponseShow = new ResponseAccesoRulesShow();

            Response responseShowAccesoRules = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (responseShowAccesoRules.estado)
            {
                AccessRuleResponseDto responseAccesoRules = JsonConvert.DeserializeObject<AccessRuleResponseDto>(responseShowAccesoRules.data);
                apiResponseShow.status = responseShowAccesoRules.estado;
                apiResponseShow.accessRulesDtos = responseAccesoRules.access_rules;
            }
            else
            {
                apiResponseShow.status = responseShowAccesoRules.estado;
            }
            return apiResponseShow;
        }
         private async Task<ResponseAccesoRulesShow> RunShowAll(BodyShowAllObject bodyShowAllObject)
        {
            ResponseAccesoRulesShow apiResponseShow = new ResponseAccesoRulesShow();

            Response responseShowAccesoRules = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (responseShowAccesoRules.estado)
            {
                AccessRuleResponseDto responseAccesoRules = JsonConvert.DeserializeObject<AccessRuleResponseDto>(responseShowAccesoRules.data);
                apiResponseShow.status = responseShowAccesoRules.estado;
                apiResponseShow.accessRulesDtos = responseAccesoRules.access_rules;
            }
            else
            {
                apiResponseShow.status = responseShowAccesoRules.estado;
            }
            return apiResponseShow;
        }
        private async Task<ResponseAccesoRulesUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseAccesoRulesUpdate apiResponseUpdate = new ResponseAccesoRulesUpdate();

            Response responseUpdateUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (responseUpdateUsers.estado)
            {
                access_rulesResponseUpdateDto apiresponseUpdate = JsonConvert.DeserializeObject<access_rulesResponseUpdateDto>(responseUpdateUsers.data);
                apiResponseUpdate.status = responseUpdateUsers.estado;
                apiResponseUpdate.changes = apiresponseUpdate.changes;
            }
            else
            {
                apiResponseUpdate.status = responseUpdateUsers.estado;
            }
            return apiResponseUpdate;
        }
        private async Task<ResponseAccesoRulesDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseAccesoRulesDelete responseDelete = new ResponseAccesoRulesDelete();

            Response APIresponseDelete = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (APIresponseDelete.estado)
            {
                access_rulesResponseDeleteDto responseDeleteDto = JsonConvert.DeserializeObject<access_rulesResponseDeleteDto>(APIresponseDelete.data);
                responseDelete.status = APIresponseDelete.estado;
                responseDelete.changes = responseDeleteDto.changes;
            }
            else
            {
                responseDelete.status = APIresponseDelete.estado;
            }
            return responseDelete;
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
        public int changes { get; set; }
    }
    public class ResponseAccesoRulesDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}