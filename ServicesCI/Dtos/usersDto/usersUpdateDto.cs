namespace ControlIDMvc.ServicesCI.Dtos.usersDto
{
    public class userUpdateDto
    {
        public values values { get; set; }
        public where where { get; set; }
    }
    public class where
    {
        public users users { get; set; }
    }
    public class values
    {
        public int begin_time { get; set; }
        public int end_time { get; set; }
    }
    public class users
    {
        public int id { get; set; }
    }
}