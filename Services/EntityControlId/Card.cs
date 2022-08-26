namespace ControlIDMvc.Services.ControlId
{
    public class Card
    {
        public int Id { get; set; }
        public int value { get; set; }
        public int user_id { get; set; }
        public User user { get; set; }
    }
}