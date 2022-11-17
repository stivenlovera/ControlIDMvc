using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.registroRostroDto;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class RegistroRostroControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public RegistroRostroControlIdQuery(HttpClientService httpClientService)
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
        public async Task<ResponseRegistroFotoCreate> Create(int user_id, string imagen,long fechaCreacion)
        {
            var imagenes = new List<user_images>();
            imagenes.Add(new user_images
            {
                image = imagen,
                timestamp = fechaCreacion,
                user_id = user_id
            });
            var body = new
            {
                match = true,
                user_images = imagenes

            };
            var response = await this.RunCreate(body);
            return response;
        }
        private async Task<ResponseRegistroFotoCreate> RunCreate(Object body)
        {
            ResponseRegistroFotoCreate responseCreate = new ResponseRegistroFotoCreate();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.apiUrlCreateUserImagen, body, this.session);
            if (apiResponse.estado)
            {
                responseCreateRegsitroRostroDto responseUser = JsonConvert.DeserializeObject<responseCreateRegsitroRostroDto>(apiResponse.data);
                responseCreate.status = apiResponse.estado;
                responseCreate.resultado = responseUser.results;
            }
            else
            {
                responseCreate.status = apiResponse.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseRegistroFotoShow> RunShow(Object body)
        {
            ResponseRegistroFotoShow responseShow = new ResponseRegistroFotoShow();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.apiUrlShowUserImagen, body, this.session);
            if (apiResponse.estado)
            {
                responseRegistroRostroDto responseUserImagen = JsonConvert.DeserializeObject<responseRegistroRostroDto>(apiResponse.data);
                responseShow.status = apiResponse.estado;
                responseShow.responseRegistroRostroDto = responseUserImagen;
            }
            else
            {
                responseShow.status = apiResponse.estado;
            }
            return responseShow;
        }
        private async Task<ResponseRegistroFotoDelete> RunDelete(Object body)
        {
            ResponseRegistroFotoDelete responseDelete = new ResponseRegistroFotoDelete();

            Response apiResponse = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.apiUrlDeleteUserImagen, body, this.session);
            if (apiResponse.estado)
            {
                responseDeleteRegistroRostroDto responseUserImagen = JsonConvert.DeserializeObject<responseDeleteRegistroRostroDto>(apiResponse.data);
                responseDelete.status = apiResponse.estado;
                responseDelete.responseDeleteRegistroRostroDto = responseUserImagen;
            }
            else
            {
                responseDelete.status = apiResponse.estado;
            }
            return responseDelete;
        }
    }
    public class ResponseRegistroFotoCreate
    {
        public bool status { get; set; }
        public resultado resultado { get; set; }
    }
    public class ResponseRegistroFotoShow
    {
        public bool status { get; set; }
        public responseRegistroRostroDto responseRegistroRostroDto { get; set; }
    }
    public class ResponseRegistroFotoDelete
    {
        public bool status { get; set; }
        public responseDeleteRegistroRostroDto responseDeleteRegistroRostroDto { get; set; }
    }
}