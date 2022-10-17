using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DatatableHorario
    {
        
        public int id { get; set; }
        public string nombre { get; set; }
        public string lunes { get; set; }
        public string martes { get; set; }
        public string miercoles { get; set; }
        public string jueves { get; set; }
        public string viernes { get; set; }
        public string sabado { get; set; }
        public string domingo { get; set; }
    }
}