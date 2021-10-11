using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KombinacaoMineira.Models;

namespace KombinacaoMineira.Data
{
    public class KombinacaoMineiraContext : DbContext
    {
        public KombinacaoMineiraContext (DbContextOptions<KombinacaoMineiraContext> options)
            : base(options)
        {
        }

        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Vendedor> Vendedor { get; set; }
        public DbSet<RegistroVenda> RegistroVenda { get; set; }

    }
}
