using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.Dtos.portalsAccessRulesDto;
using ControlIDMvc.ServicesCI.portalsAccessRulesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class PortalsAccessRulesControlIdQuery
    {

        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public PortalsAccessRulesControlIdQuery(HttpClientService httpClientService)
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

        public async Task<ResponsPortalAccessRulesCreate> Create(PortalReglaAcceso portalReglaAcceso)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "portal_access_rules",
                values = new List<portalsAccessRulesCreateDto>(){
                    new portalsAccessRulesCreateDto{
                        access_rule_id=portalReglaAcceso.ControlIdRulesId,
                        portal_id=portalReglaAcceso.ControlIdPortalId
                    }
                }
            };
            //return body;
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponsPortalAccessRulesCreate> CreateAll(List<PortalReglaAcceso> portalReglaAccesos)
        {
            var data = new List<portalsAccessRulesCreateDto>();
            foreach (var portalReglaAcceso in portalReglaAccesos)
            {
                data.Add(new portalsAccessRulesCreateDto
                {
                    access_rule_id = portalReglaAcceso.ControlIdRulesId,
                    portal_id = portalReglaAcceso.ControlIdPortalId
                });
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "portal_access_rules",
                values = data
            };
            //return body;
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponsPortalAccessRulesShow> ShowAll()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "portal_access_rules",
            };
            var response = await this.RunShow(body);
            return response;
        }
        public async Task<ResponsePortalAccessRulesDelete> DeleteAccessRulesId(List<PortalReglaAcceso> portalReglaAccesos)
        {
            var PortalReglaAccesoIds = new List<int>();
            foreach (var portalReglaAcceso in portalReglaAccesos)
            {
                PortalReglaAccesoIds.Add(portalReglaAcceso.ControlIdRulesId);
            }
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "portal_access_rules",
                where = new
                {
                    portal_access_rules = new
                    {
                        access_rule_id = PortalReglaAccesoIds
                    }
                }
            };
            var response = await this.RunDelete(body);
            return response;
        }
        public async Task<ResponsePortalAccessRulesDelete> DeleteAll()
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "portal_access_rules"
            };
            var response = await this.RunDelete(body);
            return response;
        }

        private async Task<ResponsPortalAccessRulesCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponsPortalAccessRulesCreate responsePortalAccessRules = new ResponsPortalAccessRulesCreate();
            Response apiResponseAddPortalAccessRules = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (apiResponseAddPortalAccessRules.estado)
            {
                portalsAccessRulesResponseDto response = JsonConvert.DeserializeObject<portalsAccessRulesResponseDto>(apiResponseAddPortalAccessRules.data);
                responsePortalAccessRules.status = apiResponseAddPortalAccessRules.estado;
                responsePortalAccessRules.ids = response.ids;
            }
            else
            {
                responsePortalAccessRules.status = false;
            }
            return responsePortalAccessRules;
        }
        private async Task<ResponsPortalAccessRulesShow> RunShow(BodyShowObject bodyShowAllObject)
        {
            ResponsPortalAccessRulesShow responseShow = new ResponsPortalAccessRulesShow();

            Response apiResponseUpdate = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (apiResponseUpdate.estado)
            {
                portalAccesoRulesResponseDto response = JsonConvert.DeserializeObject<portalAccesoRulesResponseDto>(apiResponseUpdate.data);
                responseShow.status = apiResponseUpdate.estado;
                responseShow.portalAccesoRulesDtos = response.portal_access_rules;
            }
            else
            {
                responseShow.status = apiResponseUpdate.estado;
            }
            return responseShow;
        }
        private async Task<ResponsePortalAccessRulesUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponsePortalAccessRulesUpdate responseUpdate = new ResponsePortalAccessRulesUpdate();

            Response apiResponseupdateCards = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (apiResponseupdateCards.estado)
            {
                portalAccesoRulesResponseUpdateDto response = JsonConvert.DeserializeObject<portalAccesoRulesResponseUpdateDto>(apiResponseupdateCards.data);
                responseUpdate.status = apiResponseupdateCards.estado;
                responseUpdate.changes = response.changes;
            }
            else
            {
                responseUpdate.status = apiResponseupdateCards.estado;
            }
            return responseUpdate;
        }
        private async Task<ResponsePortalAccessRulesDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponsePortalAccessRulesDelete responseDelete = new ResponsePortalAccessRulesDelete();

            Response apiResponseDelete = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (apiResponseDelete.estado)
            {
                portalAccesoRulesResponseDeleteDto response = JsonConvert.DeserializeObject<portalAccesoRulesResponseDeleteDto>(apiResponseDelete.data);
                responseDelete.status = apiResponseDelete.estado;
                responseDelete.changes = response.changes;
            }
            else
            {
                responseDelete.status = apiResponseDelete.estado;
            }
            return responseDelete;
        }
    }
    /*extras*/
    public class ResponsPortalAccessRulesCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponsPortalAccessRulesShow
    {
        public bool status { get; set; }
        public List<portalAccesoRulesDto> portalAccesoRulesDtos { get; set; }
    }
    public class ResponsePortalAccessRulesUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
    public class ResponsePortalAccessRulesDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }

}