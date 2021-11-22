using Desafio.Dominio.Mediador.Mediadores.Consultas;
using Desafio.Infraestrutura.Dados.Contextos;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InstalarDependencias
    {
        public static void AdicionarRequisicoes(this IServiceCollection servicos, IConfiguration configuracao)
        {
            servicos.AddMediatR(typeof(VerificarUsuario).Assembly);
            servicos.AdicionarBancoDeDados<DesafioContexto>(configuracao);
        }
    }
}
