
namespace ControlIDMvc.Services.ControlId
{
    public class cards
    {
        public int id { get; set; }
        public int value { get; set; }
        public int user_id { get; set; }
    }
    public class cardsCreateDto
    {
        public int value { get; set; }
        public int user_id { get; set; }
    }
    public class cardsUpdateDto
    {
        public int value { get; set; }
    }
    public class cardsDeleteDto
    {
        public int value { get; set; }
    }
}