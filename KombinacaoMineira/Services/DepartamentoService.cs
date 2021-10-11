using KombinacaoMineira.Data;
using KombinacaoMineira.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KombinacaoMineira.Services
{
    public class DepartamentoService
    {
        private readonly KombinacaoMineiraContext _context;

        public DepartamentoService(KombinacaoMineiraContext context)
        {
            _context = context;
        }

        public async Task<List<Departamento>> EncontrarTodosAsync()   
        {
            var lista = _context.Departamento.OrderBy(p=>p.Nome).ToListAsync();
            return await lista;
        }
    }
}
