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
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "actions"
            };
            var response = await this.RunShow(body);
            return response;
        }
        public async Task<ResponseActionsShow> ShowAll()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "actions"
            };
            var response = await this.RunShowAll(body);
            return response;
        }
        private async Task<ResponseActionsShow> RunShow(BodyShowObject bodyShowObject)
        {
            ResponseActionsShow responseCreate = new ResponseActionsShow();

            Response responseActions = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (responseActions.estado)
            {
                ResponseActionsDto responseUser = JsonConvert.DeserializeObject<ResponseActionsDto>(responseActions.data);
                responseCreate.status = responseActions.estado;
                responseCreate.actionsDto = responseUser.actions;
            }
            else
            {
                responseCreate.status = responseActions.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseActionsShow> RunShowAll(BodyShowAllObject bodyShowObject)
        {
            ResponseActionsShow responseCreate = new ResponseActionsShow();

            Response responseActions = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowObject, this.session);
            if (responseActions.estado)
            {
                ResponseActionsDto responseUser = JsonConvert.DeserializeObject<ResponseActionsDto>(responseActions.data);
                responseCreate.status = responseActions.estado;
                responseCreate.actionsDto = responseUser.actions;
            }
            else
            {
                responseCreate.status = responseActions.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseActionsCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseActionsCreate responseCreate = new ResponseActionsCreate();

            Response responseAddActions = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (responseAddActions.estado)
            {
                actionsCreateResponseDto responseUser = JsonConvert.DeserializeObject<actionsCreateResponseDto>(responseAddActions.data);
                responseCreate.status = responseAddActions.estado;
                responseCreate.ids = responseUser.ids;
            }
            else
            {
                responseCreate.status = responseAddActions.estado;
            }
            return responseCreate;
        }

        private async Task<ResponseUserUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseUserUpdate responseCreate = new ResponseUserUpdate();

            Response responseUpdateUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (responseUpdateUsers.estado)
            {
                actionsResponseUpdateDto responseUser = JsonConvert.DeserializeObject<actionsResponseUpdateDto>(responseUpdateUsers.data);
                responseCreate.status = responseUpdateUsers.estado;
                responseCreate.changes = responseUser.changes;
            }
            else
            {
                responseCreate.status = responseUpdateUsers.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseActionsDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseActionsDelete responsedelete = new ResponseActionsDelete();

            Response responseDeleteActions = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (responseDeleteActions.estado)
            {
                actionsResponseDeleteDto responseUser = JsonConvert.DeserializeObject<actionsResponseDeleteDto>(responseDeleteActions.data);
                responsedelete.status = responseDeleteActions.estado;
                responsedelete.changes = responseUser.changes;
            }
            else
            {
                responsedelete.status = responseDeleteActions.estado;
            }
            return responsedelete;
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
    public class ResponseActionsUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
    public class ResponseActionsDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}