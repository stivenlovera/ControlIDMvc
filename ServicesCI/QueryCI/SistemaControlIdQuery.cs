using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.SISTEMA;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class SistemaControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public SistemaControlIdQuery(HttpClientService httpClientService)
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

        public async Task<ResponseSistemaShow> ShowInfoSistema()
        {
            BodyCreateObject body = new BodyCreateObject();
            var response = await this.RunCreate(body);
            return response;
        }
        private async Task<ResponseSistemaShow> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseSistemaShow responseCreate = new ResponseSistemaShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlGetInfoSistema, bodyCreateObject, this.session);
            if (responseAddUsers.estado)
            {
                responseInformacionSistemaDto responseUser = JsonConvert.DeserializeObject<responseInformacionSistemaDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.responseInformacionSistemaDto = responseUser;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
    }
    /*clases de ayuda*/
    public class ResponseSistemaShow
    {
        public bool status { get; set; }
        public responseInformacionSistemaDto responseInformacionSistemaDto { get; set; }
    }
}