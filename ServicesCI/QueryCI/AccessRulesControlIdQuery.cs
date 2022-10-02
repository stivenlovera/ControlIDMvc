using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.access_rulesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class AccessRulesControlIdQuery
    {
        public BodyCreateObject CreateAccessRules(List<accessRulesCreateDto> accessRules)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "access_rules",
                values = accessRules
            };
            return body;
        }
        public BodyShowObject MostrarAccessRules()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "access_rules"
            };
            return body;
        }
        public BodyShowObject MostrarOneAccessRules(int id)
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "access_rules",
                where = new
                {
                    access_rules = new
                    {
                        id = id
                    }
                },
            };
            return body;
        }
        public BodyDeleteObject DeleteAccessRules(int id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "access_rules",
                where = new
                {
                    access_rules = new
                    {
                        id = id
                    }
                },
            };
            return body;
        }
    }
}