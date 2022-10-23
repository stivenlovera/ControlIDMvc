using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos;
using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.Dtos.cardsDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class CardControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public CardControlIdQuery(HttpClientService httpClientService)
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
        public async Task<ResponseCardCreate> CreateCards(List<cardsCreateDto> tarjetas)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "cards",
                values = tarjetas
            };
            //return body;
            var response = await this.RunCreate(body);
            return response;
        }
        public BodyShowObject ShowAll()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "cards",
            };
            return body;
        }
        public BodyShowObject Show(int numero)
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "cards",
                where = new
                {
                    cards = new
                    {
                        value = numero
                    }
                }
            };
            return body;
        }
        public BodyDeleteObject DeleteUnoCard(int numero)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "cards",
                where = new
                {
                    cards = new
                    {
                        value = numero
                    }
                }
            };
            return body;
        }
        private async Task<ResponseCardCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseCardCreate responseCreate = new ResponseCardCreate();
            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject,this.session);
            
            if (responseAddUsers.estado)
            {
                cardsResponseDto responseCard = JsonConvert.DeserializeObject<cardsResponseDto>(responseAddUsers.data);
                responseCreate.status = false;
                responseCreate.ids = responseCard.ids;
            }
            else
            {
                responseCreate.status = false;
            }
            return responseCreate;
        }
    }
    public class ResponseCardCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseCardShow
    {
        public List<int> ids { get; set; }
    }
}