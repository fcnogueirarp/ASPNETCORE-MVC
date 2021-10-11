using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KombinacaoMineira.Services.Exceptions
{
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string message) : base(message)
        {
        }
    }
}
