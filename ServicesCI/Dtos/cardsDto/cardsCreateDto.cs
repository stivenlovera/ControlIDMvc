namespace ControlIDMvc.ServicesCI.Dtos.cardsDto
{
    public class cardsCreateDto
    {
        public long value { get; set; }
        public int user_id { get; set; }

    }
     public class cardsResponseDto
    {
        public List<int> ids { get; set; }
    }
}