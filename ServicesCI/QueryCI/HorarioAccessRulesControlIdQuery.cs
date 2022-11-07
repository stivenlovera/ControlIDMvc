using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using ControlIDMvc.Querys;
using ControlIDMvc.ServicesCI.Dtos.time_zones_access_rulesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class HorarioAccessRulesControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public HorarioAccessRulesControlIdQuery(HttpClientService httpClientService)
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
        
        public async Task<ResponseHorarioAccesoRulesCreate> Create(HorarioReglaAcceso horarioReglaAcceso)
        {
            List<HorarioReglaAcceso> horariosAccesoRules = new List<HorarioReglaAcceso>();
            horariosAccesoRules.Add(new HorarioReglaAcceso
            {
                 ControlIdAccessRulesId= horarioReglaAcceso.ControlIdAccessRulesId,
                 ControlIdTimeZoneId=horarioReglaAcceso.ControlIdAccessRulesId
            });
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "access_rule_time_zones",
                values = horariosAccesoRules
            };
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseHorarioAccesoRulesCreate> CreateAll(List<HorarioReglaAcceso> horarioReglaAccesos)
        {
            List<HorarioReglaAcceso> data = new List<HorarioReglaAcceso>();
            foreach (var horarioReglaAcceso in horarioReglaAccesos)
            {
                data.Add(new HorarioReglaAcceso
                {
                    ControlIdAccessRulesId = horarioReglaAcceso.ReglasAcceso.ControlId,
                    ControlIdTimeZoneId=horarioReglaAcceso.Horario.ControlId
                });
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "access_rule_time_zones",
                values = data
            };
            var response = await this.RunCreate(body);
            return response;
        }

        public async Task<ResponseHorarioAccesoRulesShow> Show()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "access_rule_time_zones"
            };
            var response = await this.RunShow(body);
            return response;
        }

        public async Task<ResponseHorarioAccesoRulesShow> ShowAll()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "access_rule_time_zones"
            };
            var response = await this.RunShowAll(body);
            return response;
        }
        public async Task<ResponseHorarioAccesoRulesUpdate> Update(HorarioReglaAcceso horarioReglaAcceso)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "access_rule_time_zones",
                values = new time_zones_access_rulesCreateDto
                {
                    access_rule_id = horarioReglaAcceso.ControlIdAccessRulesId,
                    time_zone_id=horarioReglaAcceso.ControlIdTimeZoneId
                },
                where = new
                {
                    access_rule_time_zones = new
                    {
                        access_rule_id = horarioReglaAcceso.ControlIdAccessRulesId
                    }
                }
            };
            var response = await this.RunUpdate(body);
            return response;
        }
        public async Task<ResponseHorarioAccesoRulesDelete> DeleteReglasAccesoId(int access_rule_id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "access_rule_time_zones",
                where = new
                {
                    access_rule_time_zones = new
                    {
                        access_rule_id = access_rule_id
                    }
                }
            };
            var response = await this.RunDelete(body);
            return response;
        }
        public async Task<ResponseHorarioAccesoRulesDelete> DeleteAll()
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "access_rule_time_zones",
            };
            var response = await this.RunDelete(body);
            return response;
        }

        private async Task<ResponseHorarioAccesoRulesCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseHorarioAccesoRulesCreate responseCreate = new ResponseHorarioAccesoRulesCreate();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (apiResponse.estado)
            {
                time_zones_access_ResponseCreateDto response = JsonConvert.DeserializeObject<time_zones_access_ResponseCreateDto>(apiResponse.data);
                responseCreate.status = apiResponse.estado;
                responseCreate.ids = response.ids;
            }
            else
            {
                responseCreate.status = apiResponse.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseHorarioAccesoRulesShow> RunShow(BodyShowObject bodyShowObject)
        {
            ResponseHorarioAccesoRulesShow responseShow = new ResponseHorarioAccesoRulesShow();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (apiResponse.estado)
            {
                time_zones_access_rulesResponseDto response = JsonConvert.DeserializeObject<time_zones_access_rulesResponseDto>(apiResponse.data);
                responseShow.status = apiResponse.estado;
                responseShow.time_Zones_Access_RulesDtos = response.access_rule_time_zones;
            }
            else
            {
                responseShow.status = apiResponse.estado;
            }
            return responseShow;
        }

        private async Task<ResponseHorarioAccesoRulesShow> RunShowAll(BodyShowAllObject bodyShowObject)
        {
            ResponseHorarioAccesoRulesShow responseShow = new ResponseHorarioAccesoRulesShow();

            Response apiResponseHorario = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (apiResponseHorario.estado)
            {
                time_zones_access_rulesResponseDto apiResponse = JsonConvert.DeserializeObject<time_zones_access_rulesResponseDto>(apiResponseHorario.data);
                responseShow.status = apiResponseHorario.estado;
                responseShow.time_Zones_Access_RulesDtos = apiResponse.access_rule_time_zones;
            }
            else
            {
                responseShow.status = apiResponseHorario.estado;
            }
            return responseShow;
        }
        private async Task<ResponseHorarioAccesoRulesUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseHorarioAccesoRulesUpdate responseUpdate = new ResponseHorarioAccesoRulesUpdate();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (apiResponse.estado)
            {
                time_zones_access_rulesResponseUpdateDto response = JsonConvert.DeserializeObject<time_zones_access_rulesResponseUpdateDto>(apiResponse.data);
                responseUpdate.status = apiResponse.estado;
                responseUpdate.changes = response.changes;
            }
            else
            {
                responseUpdate.status = apiResponse.estado;
            }
            return responseUpdate;
        }
        private async Task<ResponseHorarioAccesoRulesDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseHorarioAccesoRulesDelete responseDelete = new ResponseHorarioAccesoRulesDelete();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (apiResponse.estado)
            {
                time_zones_access_rulesResponseDeleteDto response = JsonConvert.DeserializeObject<time_zones_access_rulesResponseDeleteDto>(apiResponse.data);
                responseDelete.status = apiResponse.estado;
                responseDelete.changes = response.changes;
            }
            else
            {
                responseDelete.status = apiResponse.estado;
            }
            return responseDelete;
        }
    }
    public class ResponseHorarioAccesoRulesCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseHorarioAccesoRulesShow
    {
        public bool status { get; set; }
        public List<time_zones_access_rulesDto> time_Zones_Access_RulesDtos { get; set; }
    }
    public class ResponseHorarioAccesoRulesUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
    public class ResponseHorarioAccesoRulesDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}