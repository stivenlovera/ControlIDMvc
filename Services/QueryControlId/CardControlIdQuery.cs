using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlIDMvc.Entities;
using ControlIDMvc.Services.BodyControlId;
using ControlIDMvc.Services.ControlId;

namespace ControlIDMvc.Services.QueryControlId
{
    public class CardControlIdQuery
    {
        public string ApiUrl { get; set; }
        public BodyCreateObject CreateCards(List<Persona> personas,List<int> users_id)
        {
            cardsCreateDto user = new cardsCreateDto();
            foreach (var persona in personas)
            {
                user.user_id = 0;
                user.value =0;
            }
            List<cardsCreateDto> cards = new List<cardsCreateDto>();
            cards.Add(user);

            BodyCreateObject body = new BodyCreateObject()
            {
                objeto = "users",
                values = cards
            };
            return body;
        }
    }
}