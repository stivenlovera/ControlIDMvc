using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.Dtos.areasDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class AreaControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public AreaControlIdQuery(HttpClientService httpClientService)
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
        public async Task<ResponseAreaCreate> Store(Area area)
        {
            List<areaCreateDto> areaCreateDtos = new List<areaCreateDto>();
            areaCreateDtos.Add(new areaCreateDto
            {
                name = area.Nombre
            });
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "areas",
                values = areaCreateDtos
            };
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseAreaCreate> StoreAll(List<Area> areas)
        {
            List<areaCreateDto> data = new List<areaCreateDto>();
            foreach (var area in areas)
            {
                data.Add(new areaCreateDto
                {
                    name = area.ControlIdName,
                });
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "areas",
                values = data
            };
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseAreaShow> Show()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "areas"
            };
            var response = await this.RunShow(body);
            return response;
        }

        public async Task<ResponseAreaShow> ShowAll()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "areas"
            };
            var response = await this.RunShowAll(body);
            return response;
        }
        public async Task<ResponseAreaUpdate> Update(Area area)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "areas",
                values = new areaCreateDto
                {
                    name = area.ControlIdName
                },
                where = new
                {
                    areas = new
                    {
                        id = area.ControlId
                    }
                }
            };
            var response = await this.RunUpdate(body);
            return response;
        }
        public async Task<ResponseAreaDelete> Delete(Area area)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "areas",
                where = new
                {
                    areas = new
                    {
                        id = area.ControlId
                    }
                }
            };
            var response = await this.RunDelete(body);
            return response;
        }
        public async Task<ResponseAreaDelete> DeleteAll()
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "areas",
            };
            var response = await this.RunDelete(body);
            return response;
        }
        private async Task<ResponseAreaCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseAreaCreate responseCreate = new ResponseAreaCreate();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (responseAddUsers.estado)
            {
                responseareaCreateDto responseUser = JsonConvert.DeserializeObject<responseareaCreateDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.ids = responseUser.ids;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseAreaShow> RunShow(BodyShowObject bodyShowObject)
        {
            ResponseAreaShow responseCreate = new ResponseAreaShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (responseAddUsers.estado)
            {
                areaResponseDto responseUser = JsonConvert.DeserializeObject<areaResponseDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.areaResponseDtos = responseUser.areas;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseAreaShow> RunShowAll(BodyShowAllObject bodyShowObject)
        {
            ResponseAreaShow responseShow = new ResponseAreaShow();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (apiResponse.estado)
            {
                areaResponseDto response = JsonConvert.DeserializeObject<areaResponseDto>(apiResponse.data);
                responseShow.status = apiResponse.estado;
                responseShow.areaResponseDtos = response.areas;
            }
            else
            {
                responseShow.status = apiResponse.estado;
            }
            return responseShow;
        }
        private async Task<ResponseAreaUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseAreaUpdate responseUpdate = new ResponseAreaUpdate();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (apiResponse.estado)
            {
                areasResponseUpdateDto response = JsonConvert.DeserializeObject<areasResponseUpdateDto>(apiResponse.data);
                responseUpdate.status = apiResponse.estado;
                responseUpdate.changes = response.changes;
            }
            else
            {
                responseUpdate.status = apiResponse.estado;
            }
            return responseUpdate;
        }
        private async Task<ResponseAreaDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseAreaDelete responseDelete = new ResponseAreaDelete();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (apiResponse.estado)
            {
                areaResponseDeleteDto response = JsonConvert.DeserializeObject<areaResponseDeleteDto>(apiResponse.data);
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
    public class ResponseAreaCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseAreaShow
    {
        public bool status { get; set; }
        public List<areaDto> areaResponseDtos { get; set; }
    }
    public class ResponseAreaUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
    public class ResponseAreaDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}