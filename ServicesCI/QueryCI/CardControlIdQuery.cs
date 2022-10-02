using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Dtos;
using ControlIDMvc.Entities;
using ControlIDMvc.ServicesCI.Dtos.cardsDto;
using ControlIDMvc.ServicesCI.UtilidadesCI;

namespace ControlIDMvc.ServicesCI.QueryCI
{
    public class CardControlIdQuery
    {
        public BodyCreateObject CreateCards(List<cardsCreateDto> tarjetas)
        {
            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "cards",
                values = tarjetas
            };
            return body;
        }
        public BodyShowObject MostrarTodoCards()
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "cards",
            };
            return body;
        }
        public BodyShowObject MostrarUnoCard(int numero)
        {
            BodyShowObject body = new BodyShowObject()
            {
                objeto = "cards",
                where = new
                {
                    cards = new
                    {
                        value = numero
                    }
                }
            };
            return body;
        }
        public BodyDeleteObject DeleteUnoCard(int numero)
        {
            BodyDeleteObject body = new BodyDeleteObject()
            {
                objeto = "cards",
                where = new
                {
                    cards = new
                    {
                        value = numero
                    }
                }
            };
            return body;
        }
    }
}