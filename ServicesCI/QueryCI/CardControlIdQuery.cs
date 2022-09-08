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
        public string ApiUrl { get; set; }
        public BodyCreateObject CreateCards(List<PersonaCreateDto> personas, List<int> users_id)
        {
            cardsCreateDto card = new cardsCreateDto();

            foreach (var persona in personas)
            {
                int i = 0;
                foreach (var area in persona.Area)
                {
                    card.user_id = users_id[i];
                    int area_convert = Int32.Parse(area);
                    int area_codigo = Int32.Parse(persona.Codigo[i]);
                    /*calculo*/
                   card.value = (area_convert * Convert.ToInt64((Math.Pow(2, 32))))+ area_codigo;
                    i++;
                }

            }
            List<cardsCreateDto> cards = new List<cardsCreateDto>();
            cards.Add(card);

            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "cards",
                values = cards
            };
            return body;
        }
    }
}