using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.portalsDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class PortalsControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public PortalsControlIdQuery(HttpClientService httpClientService)
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
        public async Task<ResponsePortalShow> ShowAll()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "portals"
            };
            var response = await this.RunShow(body);
            return response;
        }
        public BodyUpdateObject UpdatePortals(portalsCreateDto portalsCreateDto, int portal_id)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "portals",
                values = portalsCreateDto,
                where = new
                {
                    portals = new
                    {
                        id = portal_id
                    }
                }
            };
            return body;
        }
        private async Task<ResponsePortalShow> RunShow(BodyShowAllObject bodyShowAllObject)
        {
            ResponsePortalShow responseCreate = new ResponsePortalShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (responseAddUsers.estado)
            {
                ResponseportalsDto responseUser = JsonConvert.DeserializeObject<ResponseportalsDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.portalsDtos = responseUser.portals;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
    }

    /*clases de ayuda*/
    public class ResponsePortalCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponsePortalShow
    {
        public bool status { get; set; }
        public List<portalsDto> portalsDtos { get; set; }
    }
    
}