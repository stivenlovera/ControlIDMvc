using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.Dtos.area_access_rulesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{



    public class AreaAccesRuleControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public AreaAccesRuleControlIdQuery(HttpClientService httpClientService)
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
        /*Crud Api*/
        public async Task<ResponseAreaReglasAccessCreate> Store(AreaReglaAcceso areaReglaAcceso)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "area_access_rules",
                values = new List<AreaReglaAcceso>(){
                    new AreaReglaAcceso{
                        ControlIdAreaId=areaReglaAcceso.ControlIdAreaId,
                        ControlidReglaAccesoId=areaReglaAcceso.ControlidReglaAccesoId
                    }
                }
            };
            //return body;
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseAreaReglasAccessCreate> CreateAll(List<AreaReglaAcceso> areaReglaAccesos)
        {
            var data = new List<area_access_rulesCreateDto>();
            foreach (var areaReglaAcceso in areaReglaAccesos)
            {
                data.Add(new area_access_rulesCreateDto
                {
                    access_rule_id=areaReglaAcceso.ReglaAcceso.ControlId,
                    area_id=areaReglaAcceso.Area.ControlId
                });
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "area_access_rules",
                values = data
            };
            //return body;
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseAreaReglasAccessShow> ShowAll()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "area_access_rules",
            };
            var response = await this.RunShowAll(body);
            return response;
        }
        public async Task<ResponseAreaReglasAccessUpdate> Update(AreaReglaAcceso areaReglaAcceso)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "area_access_rules",
                values = new area_access_rulesCreateDto
                {
                  area_id=areaReglaAcceso.ControlIdAreaId,
                  access_rule_id=areaReglaAcceso.ControlidReglaAccesoId
                },
                where = new
                {
                    area_access_rules = new
                    {
                        id = areaReglaAcceso.ControlIdAreaId
                    }
                }
            };
            var response = await this.RunUpdate(body);
            return response;
        }
        
        public async Task<ResponseAreaReglasAccessDelete> DeleteAccessRulesId(int access_rule_id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "area_access_rules",
                where = new
                {
                    area_access_rules = new
                    {
                        access_rule_id = access_rule_id
                    }
                }
            };
            var response = await this.RunDelete(body);
            return response;
        }
        
        public async Task<ResponseAreaReglasAccessDelete> DeleteAll()
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "area_access_rules"
            };
            var response = await this.RunDelete(body);
            return response;
        }

        /*run*/
         private async Task<ResponseAreaReglasAccessCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseAreaReglasAccessCreate responseCreate = new ResponseAreaReglasAccessCreate();
            Response apiRespose = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (apiRespose.estado)
            {
                area_access_rulesResponseCreateDto response = JsonConvert.DeserializeObject<area_access_rulesResponseCreateDto>(apiRespose.data);
                responseCreate.status = apiRespose.estado;
                responseCreate.ids = response.ids;
            }
            else
            {
                responseCreate.status = apiRespose.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseAreaReglasAccessShow> RunShow(BodyShowObject bodyShowAllObject)
        {
            ResponseAreaReglasAccessShow responseShow = new ResponseAreaReglasAccessShow();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (apiResponse.estado)
            {
                area_access_rulesResponseDto response = JsonConvert.DeserializeObject<area_access_rulesResponseDto>(apiResponse.data);
                responseShow.status = apiResponse.estado;
                responseShow.area_Access_RulesControlDtos = response.area_access_rules;
            }
            else
            {
                responseShow.status = apiResponse.estado;
            }
            return responseShow;
        }
        private async Task<ResponseAreaReglasAccessShow> RunShowAll(BodyShowAllObject bodyShowAllObject)
        {
            ResponseAreaReglasAccessShow responseShow = new ResponseAreaReglasAccessShow();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (apiResponse.estado)
            {
                area_access_rulesResponseDto response = JsonConvert.DeserializeObject<area_access_rulesResponseDto>(apiResponse.data);
                responseShow.status = apiResponse.estado;
                responseShow.area_Access_RulesControlDtos = response.area_access_rules;
            }
            else
            {
                responseShow.status = apiResponse.estado;
            }
            return responseShow;
        }
        private async Task<ResponseAreaReglasAccessUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseAreaReglasAccessUpdate responseUpdate = new ResponseAreaReglasAccessUpdate();

            Response responseupdateCards = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (responseupdateCards.estado)
            {
                area_access_rulesResponseUpdateDto responseUser = JsonConvert.DeserializeObject<area_access_rulesResponseUpdateDto>(responseupdateCards.data);
                responseUpdate.status = responseupdateCards.estado;
                responseUpdate.changes = responseUser.changes;
            }
            else
            {
                responseUpdate.status = responseupdateCards.estado;
            }
            return responseUpdate;
        }
        private async Task<ResponseAreaReglasAccessDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseAreaReglasAccessDelete responseDelete = new ResponseAreaReglasAccessDelete();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (apiResponse.estado)
            {
                area_access_rulesResponseDeleteDto responseUser = JsonConvert.DeserializeObject<area_access_rulesResponseDeleteDto>(apiResponse.data);
                responseDelete.status = apiResponse.estado;
                responseDelete.changes = responseUser.changes;
            }
            else
            {
                responseDelete.status = apiResponse.estado;
            }
            return responseDelete;
        }    

    }

    public class ResponseAreaReglasAccessCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseAreaReglasAccessShow
    {
        public bool status { get; set; }
        public List<area_access_rulesControlDto> area_Access_RulesControlDtos { get; set; }
    }
    public class ResponseAreaReglasAccessUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
    public class ResponseAreaReglasAccessDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}