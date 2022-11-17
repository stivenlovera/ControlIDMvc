using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.registroRostroDto
{
    public class createRegsitroRostroDto
    {
        public bool match { get; set; }
        public List<user_images> user_images { get; set; }
    }
    public class user_images
    {
        public int user_id { get; set; }
        public long timestamp { get; set; }
        public string image { get; set; }
    }
    public class responseCreateRegsitroRostroDto
    {
        public resultado results { get; set; }
    }
    public class resultado
    {
        public int user_id { get; set; }
        public scores scores { get; set; }
        public bool success { get; set; }
    }
    public class scores 
    {
        public int bounds_width { get; set; }
        public int horizontal_center_offset { get; set; }
        public int vertical_center_offset { get; set; }
        public int center_pose_quality { get; set; }
        public int sharpness_quality { get; set; }
    }
}