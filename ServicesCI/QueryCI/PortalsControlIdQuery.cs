using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.portalsDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class PortalsControlIdQuery
    {
        public BodyShowAllObject ShowPortals()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "portals"
            };
            return body;
        }
        public BodyUpdateObject UpdatePortals(portalsCreateDto portalsCreateDto, int portal_id)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "portals",
                values = portalsCreateDto,
                where = new
                {
                    portals = new
                    {
                        id = portal_id
                    }
                }
            };
            return body;
        }
    }
}