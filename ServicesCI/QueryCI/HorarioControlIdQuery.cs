using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.Dtos.time_spansDto;
using ControlIDMvc.ServicesCI.Dtos.time_zonesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;
using static ControlIDMvc.ServicesCI.Dtos.time_zonesDto.timezoneDelete;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    /*crear horario en la api*/
    public class HorarioControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public HorarioControlIdQuery(HttpClientService httpClientService)
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

        public async Task<ResponseHorarioCreate> Create(Horario horario)
        {
            List<time_zonesCreateDto> horarios = new List<time_zonesCreateDto>();
            horarios.Add(new time_zonesCreateDto
            {
                name = horario.Nombre
            });
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "time_zones",
                values = horarios
            };
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseHorarioCreate> CreateAll(List<Horario> horarios)
        {
            List<time_zonesCreateDto> data = new List<time_zonesCreateDto>();
            foreach (var horario in horarios)
            {
                data.Add(new time_zonesCreateDto
                {
                    name = horario.ControlIdName
                });
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "time_zones",
                values = data
            };
            var response = await this.RunCreate(body);
            return response;
        }

        public async Task<ResponseHorarioShow> Show()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "time_zones"
            };
            var response = await this.RunShow(body);
            return response;
        }

        public async Task<ResponseHorarioShow> ShowAll()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "time_zones"
            };
            var response = await this.RunShowAll(body);
            return response;
        }
        public async Task<ResponseHorarioUpdate> Update(Horario horario)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "time_zones",
                values = new time_zonesCreateDto
                {
                    name = horario.Nombre
                },
                where = new
                {
                    time_zones = new
                    {
                        id = horario.ControlId
                    }
                }
            };
            var response = await this.RunUpdate(body);
            return response;
        }
        public async Task<ResponseHorarioDelete> Delete(Horario horario)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "time_zones",
                where = new
                {
                    time_zones = new
                    {
                        id = horario.ControlId
                    }
                }
            };
            var response = await this.RunDelete(body);
            return response;
        }
        public async Task<ResponseHorarioDelete> DeleteAll()
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "time_zones",
            };
            var response = await this.RunDelete(body);
            return response;
        }
        private async Task<ResponseHorarioCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseHorarioCreate responseCreate = new ResponseHorarioCreate();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (apiResponse.estado)
            {
                time_zonesResponseCreateDto responseUser = JsonConvert.DeserializeObject<time_zonesResponseCreateDto>(apiResponse.data);
                responseCreate.status = apiResponse.estado;
                responseCreate.ids = responseUser.ids;
            }
            else
            {
                responseCreate.status = apiResponse.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseHorarioShow> RunShow(BodyShowObject bodyShowObject)
        {
            ResponseHorarioShow responseShow = new ResponseHorarioShow();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (apiResponse.estado)
            {
                time_zonesResponseDto response = JsonConvert.DeserializeObject<time_zonesResponseDto>(apiResponse.data);
                responseShow.status = apiResponse.estado;
                responseShow.timezoneDtos = response.time_zones;
            }
            else
            {
                responseShow.status = apiResponse.estado;
            }
            return responseShow;
        }

        private async Task<ResponseHorarioShow> RunShowAll(BodyShowAllObject bodyShowObject)
        {
            ResponseHorarioShow responseShow = new ResponseHorarioShow();

            Response apiResponseHorario = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (apiResponseHorario.estado)
            {
                time_zonesResponseDto apiResponse = JsonConvert.DeserializeObject<time_zonesResponseDto>(apiResponseHorario.data);
                responseShow.status = apiResponseHorario.estado;
                responseShow.timezoneDtos = apiResponse.time_zones;
            }
            else
            {
                responseShow.status = apiResponseHorario.estado;
            }
            return responseShow;
        }
        private async Task<ResponseHorarioUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseHorarioUpdate responseUpdate = new ResponseHorarioUpdate();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (apiResponse.estado)
            {
                timezoneResponseUpdateDto response = JsonConvert.DeserializeObject<timezoneResponseUpdateDto>(apiResponse.data);
                responseUpdate.status = apiResponse.estado;
                responseUpdate.changes = response.changes;
            }
            else
            {
                responseUpdate.status = apiResponse.estado;
            }
            return responseUpdate;
        }
        private async Task<ResponseHorarioDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseHorarioDelete responseDelete = new ResponseHorarioDelete();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (apiResponse.estado)
            {
                timezoneResponseDeleteDto response = JsonConvert.DeserializeObject<timezoneResponseDeleteDto>(apiResponse.data);
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
    public class ResponseHorarioCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseHorarioShow
    {
        public bool status { get; set; }
        public List<timezoneDto> timezoneDtos { get; set; }
    }
    public class ResponseHorarioUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
    public class ResponseHorarioDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}