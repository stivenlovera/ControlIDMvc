using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.actionsDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class ActionsControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public ActionsControlIdQuery(HttpClientService httpClientService)
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
        public async Task<ResponseActionsShow> ShowActions()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "actions"
            };
            var response = await this.RunShow(body);
            return response;
        }
        private async Task<ResponseActionsShow> RunShow(BodyShowAllObject bodyShowAllObject)
        {
            ResponseActionsShow responseCreate = new ResponseActionsShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (responseAddUsers.estado)
            {
                ResponseActionsDto responseUser = JsonConvert.DeserializeObject<ResponseActionsDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.actionsDto = responseUser.actions;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
    }
    /*clases de ayuda*/
    public class ResponseActionsCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseActionsShow
    {
        public bool status { get; set; }
        public List<actionsDto> actionsDto { get; set; }
    }
}