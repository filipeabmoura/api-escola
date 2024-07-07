using api_escola.Model;
using Microsoft.EntityFrameworkCore;

namespace api_escola.Context
{
    public class BancoDeDados : DbContext
    {
        public BancoDeDados(DbContextOptions<BancoDeDados> options)
            : base(options) { }

        public DbSet<Escola> Escolas => Set<Escola>();
    }
}
