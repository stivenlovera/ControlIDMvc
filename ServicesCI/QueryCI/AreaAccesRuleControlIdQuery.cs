using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.area_access_rulesDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class AreaAccesRuleControlIdQuery
    {
        public BodyCreateObject CreateAccesRule(List<areaAccessControlDto> areaAccessControlDtos)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "area_access_rules",
                values = areaAccessControlDtos
            };
            return body;
        }
        public BodyDeleteObject DeleteAreas(List<int> area_id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "area_access_rules",
                where = new
                {
                    area_access_rules = new
                    {
                        area_id = area_id
                    }
                }
            };
            return body;
        }
    }
}