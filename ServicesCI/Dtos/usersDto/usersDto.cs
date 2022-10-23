using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.usersDto
{
    public class usersDto
    {
        public int id { get; set; }
        public string registration { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public int begin_time { get; set; } = 2;
        public int end_time { get; set; } = 2;
    }
    public class usersResponseDto
    {
        public List<usersDto> usersDtos { get; set; }
    }
}