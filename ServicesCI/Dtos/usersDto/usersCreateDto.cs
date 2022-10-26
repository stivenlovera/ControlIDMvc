namespace ControlIDMvc.ServicesCI.Dtos.usersDto
{
    public class usersCreateDto
    {
        public string registration { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public long begin_time { get; set; } = 2;
        public long end_time { get; set; } = 2;
    }
    public class usersResponseCreateDto
    {
        public List<int> ids { get; set; }
    }
}