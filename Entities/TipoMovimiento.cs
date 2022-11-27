using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class TipoMovimiento
    {
        public int Id { get; set; }
        public string NombreMovimiento { get; set; }
        public List<MovimientosAsiento> MovimientosAsiento { get; set; }
        
    }
}