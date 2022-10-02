using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.portalsAccessRulesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class PortalsAccessRulesControlIdQuery
    {
        public BodyCreateObject CreatePortalAccessRules(List<portalsAccessRulesCreateDto> portalesAccessRules)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "portal_access_rules",
                values = portalesAccessRules
            };
            return body;
        }
        public BodyShowObject MostrarPortalAccessRules()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "portal_access_rules"
            };
            return body;
        }
        public BodyShowObject MostrarUnoPortal(int portal_id)
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "portal_access_rules",
                where = new
                {
                    portal_access_rules = new
                    {
                        portal_id = portal_id,
                    }
                }
            };
            return body;
        }
        public BodyShowObject MostrarUnoAccessRules(int acceso_rules_id)
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "portal_access_rules",
                where = new
                {
                    portal_access_rules = new
                    {
                        acceso_rules_id = acceso_rules_id,
                    }
                }
            };
            return body;
        }
        public BodyDeleteObject DeletePortalAccessRules(int portal_id, int acceso_rules_id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "portal_access_rules",
                where = new
                {
                    portal_access_rules =
                    new
                    {
                        portal_id = portal_id,
                        access_rule_id = acceso_rules_id
                    }
                }
            };
            return body;
        }
    }
}