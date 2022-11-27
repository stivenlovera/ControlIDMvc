using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.ReciboEgresoDto
{
    public class ReciboEgresoDto
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string NroRecibo { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public string MontoLiteral { get; set; }
        public string EntregeA { get; set; }
        public string NombrePersona { get; set; }
        public List<Items> Items { get; set; }
    }
    public class Items
    {
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public int Facturacion { get; set; }
        public List<Planes> Planes { get; set; }
    }
    public class Planes
    {
        public string PlanId { get; set; }
        public string PlanCuenta { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber  { get; set; }
    }
}