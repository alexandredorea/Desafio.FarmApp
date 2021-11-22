using Desafio.Infraestrutura.Dados.Contextos;
using Desafio.Infraestrutura.Dados.Repositorios.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InstalarDependencias
    {
        public static void AdicionarBancoDeDados<TipoContexto, ChavePrimaria>(this IServiceCollection servicos, IConfiguration configuracao)
            where TipoContexto : DesafioContexto
        {
            servicos.AddDbContext<TipoContexto>(opcoes => opcoes.UseInMemoryDatabase("DesafioEmMemoria"));
            servicos.AddScoped<IRepositorioBase<ChavePrimaria>, RepositorioBase<TipoContexto, ChavePrimaria>>();
        }

        public static void AdicionarBancoDeDados<TipoContexto>(this IServiceCollection servicos, IConfiguration configuracao)
            where TipoContexto : DesafioContexto
        {
            servicos.AddDbContext<TipoContexto>(opcoes => opcoes.UseInMemoryDatabase("DesafioEmMemoria"));
            servicos.AddScoped<IRepositorioBase, RepositorioBase<TipoContexto>>();
        }

    }
}
