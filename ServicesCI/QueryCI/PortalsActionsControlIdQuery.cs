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
                objeto = "portal_actions"
            };
            var response = await this.RunShow(body);
            return response;
        }
        private async Task<ResponsePortalActionShow> RunShow(BodyShowAllObject bodyShowAllObject)
        {
            ResponsePortalActionShow responseShow = new ResponsePortalActionShow();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject , this.session);
            if (apiResponse.estado)
            {
                responsePortalsActionsDto responseUser = JsonConvert.DeserializeObject<responsePortalsActionsDto>(apiResponse.data);
                responseShow.status = apiResponse.estado;
                responseShow.portalsActionsDtos = responseUser.portal_actions;
            }
            else
            {
                responseShow.status = apiResponse.estado;
            }
            return responseShow;
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