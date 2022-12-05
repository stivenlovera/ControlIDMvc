using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.MovimientoDto
{
    public class MovmientosDto
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<Usuario> Usuarios { get; set; }
        public List<TipoMovimiento> TipoMovimientos { get; set; }

    }
    public class Usuario
    {
        public string Nombre { get; set; }
        public int Id { get; set; }
    }
    public class TipoMovimiento
    {
        public string Nombre { get; set; }
        public int Id { get; set; }
    }
}