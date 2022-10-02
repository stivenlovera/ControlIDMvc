using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class ActionsControlIdQuery
    {
        public BodyShowAllObject ShowActions()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "actions"
            };
            return body;
        }
    }
}