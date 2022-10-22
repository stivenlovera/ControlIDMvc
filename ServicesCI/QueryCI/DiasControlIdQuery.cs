using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ResponseDiasCreate> CreateDias(List<time_spansCreateDto> tiempos, int horario)
        {
            foreach (var tiempo in tiempos)
            {
                tiempo.time_zone_id = horario;
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "time_spans",
                values = tiempos
            };
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseDiasShow> ShowDay(List<BodyShowAllObject> tiempos, int horario)
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "time_spans"
            };
            var response = await this.RunShow(body);
            return response;
        }
        private async Task<ResponseDiasCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseDiasCreate responseCreate = new ResponseDiasCreate();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (responseAddUsers.estado)
            {
                horarioResponseDto responseUser = JsonConvert.DeserializeObject<horarioResponseDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.ids = responseUser.ids;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseDiasShow> RunShow(BodyShowAllObject bodyShowAllObject)
        {
            ResponseDiasShow responseCreate = new ResponseDiasShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (responseAddUsers.estado)
            {
                responsetime_spansDto responseUser = JsonConvert.DeserializeObject<responsetime_spansDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.time_SpansDtos = responseUser.time_SpansDtos;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
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
}