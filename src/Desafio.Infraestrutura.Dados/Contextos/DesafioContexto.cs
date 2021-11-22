using Desafio.Dominio.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Infraestrutura.Dados.Contextos
{
    public class DesafioContexto : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }

        public DesafioContexto(DbContextOptions<DesafioContexto> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DesafioContexto).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
