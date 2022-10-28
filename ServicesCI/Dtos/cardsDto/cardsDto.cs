using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.cardsDto
{
    public class cardsDto
    {
        public int Id { get; set; }
        public long value { get; set; }
        public int user_id { get; set; }
        public string secret { get; set; }
    }
    public class cardsResponseDto
    {
        public List<cardsDto> cards { get; set; }
    }
}