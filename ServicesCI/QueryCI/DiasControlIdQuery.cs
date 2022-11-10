using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.Dtos.time_spansDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class DiasControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public DiasControlIdQuery(HttpClientService httpClientService)
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
        public async Task<ResponseDiasCreate> Create(Dia dia)
        {
            var data = new List<time_spansCreateDto>();
            data.Add(new time_spansCreateDto
            {
                time_zone_id = dia.ControlTimeZoneId,
                start = dia.ControlStart,
                end = dia.ControlEnd,
                sun = dia.ControlSun,
                mon = dia.ControlMon,
                tue = dia.ControlThu,
                fri = dia.ControlFri,
                sat = dia.ControlSat,
                hol1 = dia.ControlHol1,
                hol2 = dia.ControlHol2,
                hol3 = dia.ControlHol3
            });
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "time_spans",
                values = data
            };
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseDiasCreate> CreateAll(Horario horario)
        {
            var data = new List<time_spansCreateDto>();
            foreach (var dia in horario.Dias)
            {
                data.Add(new time_spansCreateDto
                {
                    time_zone_id = horario.ControlId,
                    start = Convert.ToInt32(dia.Start.Hour) * ((Convert.ToInt32(dia.Start.Minute * 60)) == 0 ? 60 : (Convert.ToInt32(dia.Start.Minute * 60))) * 60,
                    end = Convert.ToInt32(dia.End.Hour) * ((Convert.ToInt32(dia.End.Minute * 60)) == 0 ? 60 : (Convert.ToInt32(dia.End.Minute * 60))) * 60,
                    sun = dia.Sun,
                    mon = dia.Mon,
                    thu = dia.Thu,
                    tue = dia.Tue,
                    fri = dia.Fri,
                    sat = dia.Sat,
                    wed = dia.Wed,
                    hol1 = dia.Hol1,
                    hol2 = dia.Hol2,
                    hol3 = dia.Hol3
                });
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "time_spans",
                values = data
            };
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseDiasShow> ShowAll()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "time_spans"
            };
            var response = await this.RunShowAll(body);
            return response;
        }
        public async Task<ResponseDiasUpdate> Update(Dia dia)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "time_spans",
                values = new time_spansCreateDto
                {
                    time_zone_id = dia.ControlTimeZoneId,
                    start = dia.ControlStart,
                    end = dia.ControlEnd,
                    sun = dia.ControlSun,
                    mon = dia.ControlMon,
                    wed = dia.ControlWed,
                    tue = dia.ControlTue,
                    thu = dia.ControlThu,
                    fri = dia.ControlFri,
                    sat = dia.ControlSat,
                    hol1 = dia.ControlHol1,
                    hol2 = dia.ControlHol2,
                    hol3 = dia.ControlHol3
                },
                where = new
                {
                    time_spans = new
                    {
                        id = dia.ControlId
                    }
                }
            };
            var response = await this.RunUpdate(body);
            return response;
        }
        public async Task<ResponseDiasDelete> Delete(Dia dia)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "time_spans",
                where = new
                {
                    time_spans = new
                    {
                        value = dia.ControlId
                    }
                }
            };
            var response = await this.RunDelete(body);
            return response;
        }
        public async Task<ResponseDiasDelete> DeleteAll()
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "time_spans"
            };
            var response = await this.RunDelete(body);
            return response;
        }



        private async Task<ResponseDiasCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseDiasCreate responseCreate = new ResponseDiasCreate();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (apiResponse.estado)
            {
                horarioResponseDto response = JsonConvert.DeserializeObject<horarioResponseDto>(apiResponse.data);
                responseCreate.status = apiResponse.estado;
                responseCreate.ids = response.ids;
            }
            else
            {
                responseCreate.status = apiResponse.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseDiasShow> RunShow(BodyShowObject bodyShowObject)
        {
            ResponseDiasShow responseShow = new ResponseDiasShow();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (apiResponse.estado)
            {
                responsetime_spansDto response = JsonConvert.DeserializeObject<responsetime_spansDto>(apiResponse.data);
                responseShow.status = apiResponse.estado;
                responseShow.time_SpansDtos = response.time_spans;
            }
            else
            {
                responseShow.status = apiResponse.estado;
            }
            return responseShow;
        }
        private async Task<ResponseDiasShow> RunShowAll(BodyShowAllObject bodyShowAllObject)
        {
            ResponseDiasShow responseShow = new ResponseDiasShow();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (apiResponse.estado)
            {
                responsetime_spansDto response = JsonConvert.DeserializeObject<responsetime_spansDto>(apiResponse.data);
                responseShow.status = apiResponse.estado;
                responseShow.time_SpansDtos = response.time_spans;
            }
            else
            {
                responseShow.status = apiResponse.estado;
            }
            return responseShow;
        }
        private async Task<ResponseDiasUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseDiasUpdate responseUpdate = new ResponseDiasUpdate();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (apiResponse.estado)
            {
                time_spansResponseUpdateDto response = JsonConvert.DeserializeObject<time_spansResponseUpdateDto>(apiResponse.data);
                responseUpdate.status = apiResponse.estado;
                responseUpdate.changes = response.changes;
            }
            else
            {
                responseUpdate.status = apiResponse.estado;
            }
            return responseUpdate;
        }
        private async Task<ResponseDiasDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseDiasDelete responseDelete = new ResponseDiasDelete();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (apiResponse.estado)
            {
                time_spansResponseDeleteDto response = JsonConvert.DeserializeObject<time_spansResponseDeleteDto>(apiResponse.data);
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
    /*clases de ayuda*/
    public class ResponseDiasCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseDiasShow
    {
        public bool status { get; set; }
        public List<time_spansDto> time_SpansDtos { get; set; }
    }
    public class ResponseDiasUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
    public class ResponseDiasDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}