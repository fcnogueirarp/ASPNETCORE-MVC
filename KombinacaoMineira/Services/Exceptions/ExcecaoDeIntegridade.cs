using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KombinacaoMineira.Services.Exceptions
{
    public class ExcecaoDeIntegridade : ApplicationException
    {
        public ExcecaoDeIntegridade(string message) : base(message)
        {
        }
    }
}
