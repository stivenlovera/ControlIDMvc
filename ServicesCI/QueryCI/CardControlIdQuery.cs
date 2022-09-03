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
                foreach (var tarjeta in persona.Cards)
                {
                    card.user_id = users_id[i];
                    card.value = Int32.Parse(tarjeta);
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