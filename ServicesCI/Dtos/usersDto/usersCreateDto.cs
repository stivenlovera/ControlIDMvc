namespace ControlIDMvc.ServicesCI.Dtos.usersDto
{
    public class usersCreateDto
    {
        public string registration { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
    public class usersResponseDto
    {
        public List<int> ids { get; set; }
    }
}