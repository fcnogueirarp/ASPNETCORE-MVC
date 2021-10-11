using KombinacaoMineira.Data;
using KombinacaoMineira.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KombinacaoMineira.Services.Exceptions;

namespace KombinacaoMineira.Services
{
    public class VendedorService
    {
        private readonly KombinacaoMineiraContext _context;

        public VendedorService(KombinacaoMineiraContext context)
        {
            _context = context;
        }

        public async Task <List<Vendedor>> EncontrarTodosAsync()   //verificar - não estou seguro
        {
            var lista = _context.Vendedor.ToListAsync();
            return await lista;
        }

       public async Task InserirAsync(Vendedor obj)
        {          
            _context.Add(obj);
          await  _context.SaveChangesAsync();
        }

        public async Task<Vendedor> EncontrePorIdAsync(int id)
        {
            return  await _context.Vendedor.Include(obj=>obj.Departamento).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoverAsync(int id)
        {
            try
            {
                var obj = await _context.Vendedor.FindAsync(id);
                _context.Vendedor.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException e)
            {
                throw new ExcecaoDeIntegridade(e.Message); 
            }
        
        }

        public async Task AtualizarAsync(Vendedor obj)
        {
           if( ! await _context.Vendedor.AnyAsync(x=>x.Id== obj.Id))
            {
                throw new NotFoundException("Id não encontrada");
            }
           try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

          
        }
    }

}

    
 

