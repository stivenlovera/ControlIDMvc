using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Models.DatatableModel
{
    public class DataTablePaquete
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Dias { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal Costo { get; set; }
    }
}