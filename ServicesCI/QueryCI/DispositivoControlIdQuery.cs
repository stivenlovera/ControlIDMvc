using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.Dtos.dispositivoDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class DispositivoControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public DispositivoControlIdQuery(HttpClientService httpClientService)
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
        public async Task<ResponseDispositivoShow> ShowAll()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "devices"
            };
            var response = await this.RunShowAll(body);
            return response;
        }
        private async Task<ResponseDispositivoShow> RunShowAll(BodyShowAllObject bodyShowObject)
        {
            ResponseDispositivoShow responseShow = new ResponseDispositivoShow();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (apiResponse.estado)
            {
                DispositivoResposeDto response = JsonConvert.DeserializeObject<DispositivoResposeDto>(apiResponse.data);
                responseShow.status = apiResponse.estado;
                responseShow.dispositivoDtos = response.devices;
            }
            else
            {
                responseShow.status = apiResponse.estado;
            }
            return responseShow;
        }
        private async Task<ResponseDispositivoUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseDispositivoUpdate responseUpdate = new ResponseDispositivoUpdate();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (apiResponse.estado)
            {
                dispositivoResposeUpdateDto response = JsonConvert.DeserializeObject<dispositivoResposeUpdateDto>(apiResponse.data);
                responseUpdate.status = apiResponse.estado;
                responseUpdate.changes = response.changes;
            }
            else
            {
                responseUpdate.status = apiResponse.estado;
            }
            return responseUpdate;
        }
    }
    public class ResponseDispositivoShow
    {
        public bool status { get; set; }
        public List<DispositivoDto> dispositivoDtos { get; set; }
    }
    public class ResponseDispositivoUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}