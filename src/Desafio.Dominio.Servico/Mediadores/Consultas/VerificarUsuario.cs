using Desafio.Dominio.Mediador.Eventos;
using Desafio.Dominio.Mediador.Mediadores.Comandos;
using Desafio.Dominio.Modelos;
using Desafio.Infraestrutura.Dados.Repositorios.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.Dominio.Mediador.Mediadores.Consultas
{
    public class VerificarUsuario
    {
        public record ConsultaPorTelefone(string Telefone) : IRequest<Usuario>;
        public record ConsultaPorId(Guid Id) : IRequest<Usuario>;

        private class Mediador : 
            IRequestHandler<ConsultaPorTelefone, Usuario>,
            IRequestHandler<ConsultaPorId, Usuario>
        {
            private readonly ILogger<Mediador> logger;
            private readonly IRepositorioBase repositorio;
            private readonly IMediator mediator;

            public Mediador(
                ILogger<Mediador> logger, 
                IRepositorioBase repositorio,
                IMediator mediator)
            {
                this.logger = logger;
                this.repositorio = repositorio;
                this.mediator = mediator;
            }

            public async Task<Usuario> Handle(ConsultaPorTelefone request, CancellationToken cancellationToken)
            {
                var usuario = await repositorio.PrimeiroOuPadraoAsync<Usuario>(x => x.Telefone == request.Telefone);
                if (usuario != null)
                {
                    logger.LogInformation($"Usuário do telefone {usuario.Telefone} já existe, Id: {usuario.Id}");
                    return usuario;
                }

                logger.LogInformation($"Usuário do telefone {request.Telefone} será notificado com um Token em seu celular");
                await mediator.Send(new InserirCertificado.Comando(request.Telefone), cancellationToken);
                return null;
            }

            public async Task<Usuario> Handle(ConsultaPorId request, CancellationToken cancellationToken)
            {
                return await repositorio.ObterPorIdAsync<Usuario>(request.Id);
            }
        }
    }
}
