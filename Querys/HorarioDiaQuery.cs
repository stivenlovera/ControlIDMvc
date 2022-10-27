using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace ControlIDMvc.Querys
{
    public class HorarioDiaQuery
    {
        private readonly DBContext _dBContext;

        public HorarioDiaQuery(DBContext dBContext)
        {
            this._dBContext = dBContext;
        }
        
        
    }
}