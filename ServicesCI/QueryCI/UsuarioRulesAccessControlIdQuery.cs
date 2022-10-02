using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.user_access_rules;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class UsuarioRulesAccessControlIdQuery
    {
        public string ApiUrl { get; set; }
        public BodyCreateObject CreateUserReglaAcceso(List<userAccessRulesCreateDto> reglaAcceso)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "user_access_rules",
                values = reglaAcceso
            };
            return body;
        }
        public BodyDeleteObject DeleteReglaAcceso(int user_id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "user_access_rules",
                where = new
                {
                    user_access_rules = new
                    {
                        user_id = user_id
                    }
                }
            };
            return body;
        }
        public BodyDeleteObject DeleteUser(int access_rule_id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "user_access_rules",
                where = new
                {
                    user_access_rules = new
                    {
                        access_rule_id = access_rule_id
                    }
                }
            };
            return body;
        }
    }
}