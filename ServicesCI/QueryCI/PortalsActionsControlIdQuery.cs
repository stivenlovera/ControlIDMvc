using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class PortalsActionsControlIdQuery
    {
        public BodyUpdateObject UpdatePortalActions()
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "portal_actions",
                values = new
                {
                    action_id = 3
                },
                where = new
                {
                    portal_actions = new
                    {
                        action_id = 4
                    }
                },
            };
            return body;
        }
    }
}