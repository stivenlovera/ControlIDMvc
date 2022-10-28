using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos;
using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.Dtos.cardsDto;
using ControlIDMvc.ServicesCI.Dtos.CardsDto;
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
        public async Task<ResponseCardCreate> Create(Tarjeta tarjeta)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "cards",
                values = new List<cardsCreateDto>(){
                    new cardsCreateDto{
                        secret="",
                        user_id=tarjeta.Persona.ControlId,
                        value=this.ConvertCard(tarjeta.area.ToString(), tarjeta.codigo.ToString()),
                    }
                }
            };
            //return body;
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseCardCreate> CreateAll(List<Tarjeta> tarjetas)
        {
            var cards = new List<cardsCreateDto>();
            foreach (var tarjeta in tarjetas)
            {
                cards.Add(new cardsCreateDto
                {
                    secret = "",
                    user_id = tarjeta.Persona.ControlId,
                    value = this.ConvertCard(tarjeta.area.ToString(), tarjeta.codigo.ToString()),
                });
            }
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "cards",
                values = cards
            };
            //return body;
            var response = await this.RunCreate(body);
            return response;
        }
        public async Task<ResponseCardShow> ShowAll()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "cards",
            };
            var response = await this.RunShow(body);
            return response;
        }
        public async Task<ResponseCardShow> ShowUserId(Tarjeta tarjeta)
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "cards",
                where = new
                {
                    cards = new
                    {
                        user_id = tarjeta.Persona.ControlId
                    }
                }
            };
            var response = await this.RunShow(body);
            return response;
        }
        public async Task<ResponseCardsUpdate> Update(Tarjeta tarjeta)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "cards",
                values = new cardsCreateDto
                {
                    secret = "",
                    user_id = tarjeta.ControlIdUserId,
                    value = this.ConvertCard(tarjeta.area.ToString(), tarjeta.codigo.ToString())
                },
                where = new
                {
                    users = new
                    {
                        id = tarjeta.ControlId
                    }
                }
            };
            var response = await this.RunUpdate(body);
            return response;
        }
        public async Task<ResponseCardsDelete> Delete(Tarjeta tarjeta)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "cards",
                where = new
                {
                    cards = new
                    {
                        value = tarjeta.ControlId
                    }
                }
            };
            var response = await this.RunDelete(body);
            return response;
        }
        public async Task<ResponseCardsDelete> DeleteAllUserId(List<Tarjeta> tarjetas)
        {
            var tarjetasIds = new List<int>();
            foreach (var tarjeta in tarjetas)
            {
                tarjetasIds.Add(tarjeta.ControlId);
            }
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "cards",
                where = new
                {
                    cards = new
                    {
                        id = tarjetasIds
                    }
                }
            };
            var response = await this.RunDelete(body);
            return response;
        }
        public async Task<ResponseCardsDelete> DeleteAll()
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "cards"
            };
            var response = await this.RunDelete(body);
            return response;
        }

        private long ConvertCard(string area, string codigo)
        {
            int area_convert = Int32.Parse(area);
            int area_codigo = Int32.Parse(codigo);
            long calculo = (area_convert * Convert.ToInt64((Math.Pow(2, 32)))) + area_codigo;

            return calculo;
        }
        private async Task<ResponseCardCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseCardCreate responseCreate = new ResponseCardCreate();
            Response responseAddCards = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (responseAddCards.estado)
            {
                cardsCreateResponseDto responseCard = JsonConvert.DeserializeObject<cardsCreateResponseDto>(responseAddCards.data);
                responseCreate.status = true;
                responseCreate.ids = responseCard.ids;
            }
            else
            {
                responseCreate.status = false;
            }
            return responseCreate;
        }
        private async Task<ResponseCardShow> RunShow(BodyShowObject bodyShowAllObject)
        {
            ResponseCardShow responseShow = new ResponseCardShow();

            Response responseUpdate = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlMostrar, bodyShowAllObject, this.session);
            if (responseUpdate.estado)
            {
                cardsResponseDto responseUser = JsonConvert.DeserializeObject<cardsResponseDto>(responseUpdate.data);
                responseShow.status = responseUpdate.estado;
                responseShow.cardsDtos = responseUser.cards;
            }
            else
            {
                responseShow.status = responseUpdate.estado;
            }
            return responseShow;
        }
        private async Task<ResponseCardsUpdate> RunUpdate(BodyUpdateObject bodyUpdateObject)
        {
            ResponseCardsUpdate responseUpdate = new ResponseCardsUpdate();

            Response responseupdateCards = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlUpdate, bodyUpdateObject, this.session);
            if (responseupdateCards.estado)
            {
                cardsResponseUpdateDto responseUser = JsonConvert.DeserializeObject<cardsResponseUpdateDto>(responseupdateCards.data);
                responseUpdate.status = responseupdateCards.estado;
                responseUpdate.changes = responseUser.changes;
            }
            else
            {
                responseUpdate.status = responseupdateCards.estado;
            }
            return responseUpdate;
        }
        private async Task<ResponseCardsDelete> RunDelete(BodyDeleteObject bodyDeleteObject)
        {
            ResponseCardsDelete responseDelete = new ResponseCardsDelete();

            Response responseDeleteUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlDelete, bodyDeleteObject, this.session);
            if (responseDeleteUsers.estado)
            {
                cardsResponseDeleteDto responseUser = JsonConvert.DeserializeObject<cardsResponseDeleteDto>(responseDeleteUsers.data);
                responseDelete.status = responseDeleteUsers.estado;
                responseDelete.changes = responseUser.changes;
            }
            else
            {
                responseDelete.status = responseDeleteUsers.estado;
            }
            return responseDelete;
        }
    }

    public class ResponseCardCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseCardShow
    {
        public bool status { get; set; }
        public List<cardsDto> cardsDtos { get; set; }
    }
    public class ResponseCardsUpdate
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
    public class ResponseCardsDelete
    {
        public bool status { get; set; }
        public int changes { get; set; }
    }
}