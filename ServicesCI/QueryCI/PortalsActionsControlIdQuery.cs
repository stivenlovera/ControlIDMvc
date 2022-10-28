using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.portalsActionUpdateDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class PortalsActionsControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public PortalsActionsControlIdQuery(HttpClientService httpClientService)
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
        public BodyUpdateObject UpdatePortalActions()
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "portal_actions",
                values = new
                {
                    action_id = 3
                },
                where = new
                {
                    portal_actions = new
                    {
                        action_id = 4
                    }
                },
            };
            return body;
        }
        public async Task<ResponsePortalActionShow> Show()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "portals"
            };
            var response = await this.RunShow(body);
            return response;
        }
        private async Task<ResponsePortalActionShow> RunShow(BodyShowAllObject bodyShowAllObject)
        {
            ResponsePortalActionShow responseCreate = new ResponsePortalActionShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject , this.session);
            if (responseAddUsers.estado)
            {
                responsePortalsActionsDto responseUser = JsonConvert.DeserializeObject<responsePortalsActionsDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.portalsActionsDtos = responseUser.portalsActionsDtos;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
        /*clases de ayuda*/
        public class ResponsePortalActionCreate
        {
            public bool status { get; set; }
            public List<int> ids { get; set; }
        }
        public class ResponsePortalActionShow
        {
            public bool status { get; set; }
            public List<portalsActionsDto> portalsActionsDtos { get; set; }
        }
    }
}