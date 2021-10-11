using KombinacaoMineira.Data;
using KombinacaoMineira.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KombinacaoMineira.Services
{
    public class RegistroVendasService
    {
        private readonly KombinacaoMineiraContext _context;

        public RegistroVendasService(KombinacaoMineiraContext context)
        {
            _context = context;
        }

        public async Task<List<RegistroVenda>> EncontrePorDataAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in  _context.RegistroVenda select obj;
            if(minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }
            if(maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }


            return await result
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Departamento,RegistroVenda>>> EncontrePorDataGroupAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.RegistroVenda select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }

            return await result
                .Include(x => x.Vendedor)
                .Include(x => x.Vendedor.Departamento)
                .OrderByDescending(x => x.Data)
                .GroupBy(x => x.Vendedor.Departamento)
                .ToListAsync();
        }
    }
}
