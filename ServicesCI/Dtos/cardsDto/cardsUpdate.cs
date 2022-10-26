namespace ControlIDMvc.ServicesCI.Dtos.cardsDto
{
    public class cardsUpdate
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class where
    {
        public cards cards { get; set; }
    }
    public class values
    {
        public long value { get; set; }
        public int user_id { get; set; }
        public string secret { get; set; }

    }
    public class cards
    {
        public int id { get; set; }
    }
    public class cardsResponseUpdateDto
    {
        public int changes { get; set; }
    }
}