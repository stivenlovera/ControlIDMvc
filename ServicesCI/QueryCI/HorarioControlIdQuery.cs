using ControlIDMvc.ServicesCI.Dtos.time_spansDto;
using ControlIDMvc.ServicesCI.Dtos.time_zonesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;
using Newtonsoft.Json;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    /*crear horario en la api*/
    public class HorarioControlIdQuery
    {
        /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public HorarioControlIdQuery(HttpClientService httpClientService)
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
      
        public async Task<ResponseHorarioCreate> CreateHorario(time_zonesCreateDto horario)
        {
            List<time_zonesCreateDto> horarios = new List<time_zonesCreateDto>();
            horarios.Add(horario);
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "time_zones",
                values = horarios
            };
            var response = await this.RunCreate(body);
            return response;
        }

        public async Task<ResponseHorarioShow> Show()
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "users"
            };
          var response = await this.RunShow(body);
            return response;
        }

        private async Task<ResponseHorarioCreate> RunCreate(BodyCreateObject bodyCreateObject)
        {
            ResponseHorarioCreate responseCreate = new ResponseHorarioCreate();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyCreateObject, this.session);
            if (responseAddUsers.estado)
            {
                time_zonesResponseDto responseUser = JsonConvert.DeserializeObject<time_zonesResponseDto>(responseAddUsers.data);
                responseCreate.status = responseAddUsers.estado;
                responseCreate.ids = responseUser.ids;
            }
            else
            {
                responseCreate.status = responseAddUsers.estado;
            }
            return responseCreate;
        }
        private async Task<ResponseHorarioShow> RunShow(BodyUpdateObject bodyUpdateObject)
        {
            ResponseHorarioShow responseShow = new ResponseHorarioShow();

            Response responseAddUsers = await this._httpClientService.Run(this.controlador, this.port, this._ApiRutas.ApiUrlCreate, bodyUpdateObject, this.session);
            if (responseAddUsers.estado)
            {
                responsetimezoneDto responseUser = JsonConvert.DeserializeObject<responsetimezoneDto>(responseAddUsers.data);
                responseShow.status = responseAddUsers.estado;
                responseShow.timezoneDtos = responseUser.timezoneDtos;
            }
            else
            {
                responseShow.status = responseAddUsers.estado;
            }
            return responseShow;
        }

    }
    /*clases de ayuda*/
    public class ResponseHorarioCreate
    {
        public bool status { get; set; }
        public List<int> ids { get; set; }
    }
    public class ResponseHorarioShow
    {
        public bool status { get; set; }
        public List<timezoneDto> timezoneDtos { get; set; }
    }
}