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
        public async Task<ResponseAreaShow> ShowAll()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "areas"
            };
            var response = await this.RunShow(body);
            return response;
        }
        public BodyUpdateObject UpdateAreas(int id, string name)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "areas",
                values = new
                {
                    name = name
                },
                where = new
                {
                    areas = new
                    {
                        id = id
                    }
                }
            };
            return body;
        }
        public BodyDeleteObject DeleteAreas(int id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "areas",
                where = new
                {
                    areas = new
                    {
                        id = id
                    }
                }
            };
            return body;
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
        private async Task<ResponseAreaShow> RunShow(BodyShowAllObject bodyShowAllObject)
        {
            ResponseAreaShow responseCreate = new ResponseAreaShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyShowAllObject, this.session);
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
}