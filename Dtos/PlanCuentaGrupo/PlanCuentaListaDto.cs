using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Dtos.PlanCuentaGrupo
{
    public class PlanCuentaListaDto
    {
        public List<ListaPlanes> ListaPlanes { get; set; }
    }
    public class ListaPlanes
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string NombreCuenta { get; set; }
        public decimal Moneda { get; set; }
        public decimal Valor { get; set; }
        public string Nivel { get; set; }
        public string Modal { get; set; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
    }
}