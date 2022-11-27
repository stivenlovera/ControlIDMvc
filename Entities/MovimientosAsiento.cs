using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Entities
{
    public class MovimientosAsiento
    {
        public int Id { get; set; }
        public int TipoMovimientoId { get; set; }
        public TipoMovimiento TipoMovimiento { get; set; }
        public string EntregeA { get; set; }
        public string EntregeATipo { get; set; } //cliente/proveedor
        public int PersonaId { get; set; }
        public Persona Persona { get; set; }
        [Column(TypeName = "decimal(20,2)")]
        public decimal Monto { get; set; }
        public string MontoLiteral { get; set; }
        public string NroRecibo { get; set; }
        public DateTime Fecha { get; set; }
        public string NotaMovimiento { get; set; }
        public List<Asiento> Asientos { get; set; }
    }
}