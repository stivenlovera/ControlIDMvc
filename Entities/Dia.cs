using System.ComponentModel.DataAnnotations;

namespace ControlIDMvc.Entities
{
    public class Dia
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int HorarioId { get; set; }
        public Horario Horario { get; set; }
        public int ControlId { get; set; }
        public int ControlTimeZoneId { get; set; }
        public int ControlStart { get; set; }
        public int ControlEnd { get; set; }
        public int ControlSun { get; set; }
        public int ControlMon { get; set; }
        public int ControlTue { get; set; }
        public int ControlWed { get; set; }
        public int ControlThu { get; set; }
        public int ControlFri { get; set; }
        public int ControlSat { get; set; }
        public int ControlHol1 { get; set; }
        public int ControlHol2 { get; set; }
        public int ControlHol3 { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Sun { get; set; }
        public int Mon { get; set; }
        public int Tue { get; set; }
        public int Wed { get; set; }
        public int Thu { get; set; }
        public int Fri { get; set; }
        public int Sat { get; set; }
        public int Hol1 { get; set; }
        public int Hol2 { get; set; }
        public int Hol3 { get; set; }
    }
}