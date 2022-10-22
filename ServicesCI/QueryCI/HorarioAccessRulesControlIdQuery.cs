using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.time_zones_access_rulesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class HorarioAccessRulesControlIdQuery
    {
          /* propiedades */
        public int port { get; set; }
        public string controlador { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string session { get; set; }
        private readonly HttpClientService _httpClientService;
        ApiRutas _ApiRutas;
        public HorarioAccessRulesControlIdQuery(HttpClientService httpClientService)
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
        public BodyCreateObject CreateTimeZonesAccessRules(List<time_zones_access_rulesDto> time_Zones_Access_RulesDtos)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "access_rule_time_zones",
                values = time_Zones_Access_RulesDtos
            };
            return body;
        }
        public BodyShowObject MostrarTimeZonesAccessRules(List<time_zones_access_rulesDto> time_Zones_Access_RulesDtos)
        {

            BodyShowObject body = new BodyShowObject()
            {
                objeto = "access_rule_time_zones"
            };
            return body;
        }
        public BodyDeleteObject DeleteAccessRules(int access_rule_id)
        {

            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "access_rule_time_zones",
                where = new
                {
                    access_rule_time_zones = new
                    {
                        access_rule_id = access_rule_id
                    }
                }
            };
            return body;
        }
        public BodyDeleteObject DeleteTimeZone(int time_zone_id)
        {

            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "access_rule_time_zones",
                where = new
                {
                    access_rule_time_zones = new
                    {
                        time_zone_id = time_zone_id
                    }
                }
            };
            return body;
        }
    }
}