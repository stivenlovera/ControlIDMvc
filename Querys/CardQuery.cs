using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlIDMvc.Querys
{
    public class CardQuery
    {
        private readonly DBContext _dbContext;
        public CardQuery(DBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        
    }
}