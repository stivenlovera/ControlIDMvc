namespace ControlIDMvc.Services.ControlId
{
    public class users
    {
        public int id { get; set; }
        public string registration { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
      public class usersCreate
    {
        public string registration { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}