using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.ServicesCI.Dtos.areasDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class AreaControlIdQuery
    {
        public BodyCreateObject CreateAreas(List<areaCreateDto> areas)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "areas",
                values = areas
            };
            return body;
        }
        public BodyShowAllObject MostrarTodoAreas()
        {
            BodyShowAllObject body = new BodyShowAllObject()
            {
                objeto = "areas"
            };
            return body;
        }
        public BodyUpdateObject UpdateAreas(int id, string name)
        {
            BodyUpdateObject body = new BodyUpdateObject()
            {
                objeto = "areas",
                values = new
                {
                    name = name
                },
                where = new
                {
                    areas = new
                    {
                        id = id
                    }
                }
            };
            return body;
        }
        public BodyDeleteObject DeleteAreas(int id)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "areas",
                where = new
                {
                    areas = new
                    {
                        id = id
                    }
                }
            };
            return body;
        }
    }
}