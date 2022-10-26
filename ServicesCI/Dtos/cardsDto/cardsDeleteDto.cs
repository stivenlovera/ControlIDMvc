using ControlIDMvc.ServicesCI.Dtos.cardsDto;

namespace ControlIDMvc.ServicesCI.Dtos.CardsDto
{
    public class cardsDeleteDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class cardsResponseDeleteDto
    {
        public int changes { get; set; }
    }
}