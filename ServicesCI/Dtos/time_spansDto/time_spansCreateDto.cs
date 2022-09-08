namespace ControlIDMvc.ServicesCI.Dtos.time_spansDto
{
    public class time_spansCreateDto
    {
        public int time_zone_id { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int sun { get; set; }
        public int mon { get; set; }
        public int tue { get; set; }
        public int wed { get; set; }
        public int thu { get; set; }
        public int fri { get; set; }
        public int sat { get; set; }
        public int hol1 { get; set; }
        public int hol2 { get; set; }
        public int hol3 { get; set; }
    }
    public class horarioResponseDto
    {
        public List<int> ids { get; set; }
    }
}