using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.ServicesCI.Dtos.SISTEMA
{
    public class getInformacionSistemaDto
    {
        public Uptime uptime { get; set; }
        public int time { get; set; }
        public Memory memory { get; set; }
        public Liscense license { get; set; }
        public Biometric biometrics { get; set; }
        public Network network { get; set; }
        public string serial { get; set; }
        public string version { get; set; }
        public bool online { get; set; }
        public bool online_available { get; set; }
    }
    public class Uptime
    {
        public int days { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
        public int seconds { get; set; }
    }
    public class Memory
    {
        public Storage disk { get; set; }
        public Storage ram { get; set; }
    }
    /*memory*/
    public class Storage
    {
        public int free { get; set; }
        public int total { get; set; }
    }
    public class Liscense
    {
        public int users { get; set; }
        public int device { get; set; }
        public int type { get; set; }
    }
    public class Biometric
    {
        public int max_num_records { get; set; }
    }
    public class Network
    {
        public string mac { get; set; }
        public string ip { get; set; }
        public string netmask { get; set; }
        public string gateway { get; set; }
        public int web_server_port { get; set; }
        public bool ssl_enabled { get; set; }
        public bool ten_mbps { get; set; }
    }
}
