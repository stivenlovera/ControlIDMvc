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
        public long begin_time { get; set; }
        public long end_time { get; set; }
    }
    public class users
    {
        public int id { get; set; }
    }
    public class usersResponseUpdateDto
    {
        public int changes { get; set; }
    }

}