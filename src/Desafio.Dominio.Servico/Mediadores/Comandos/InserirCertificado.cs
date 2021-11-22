using Desafio.Dominio.Mediador.Eventos;
using Desafio.Dominio.Modelos;
using Desafio.Infraestrutura.Dados.Repositorios.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.Dominio.Mediador.Mediadores.Comandos
{
    public class InserirCertificado
    {
        public record Comando(string Telefone) : IRequest;

        private class Mediador : AsyncRequestHandler<Comando>
        {
            private readonly ILogger<Mediador> logger;
            private readonly IRepositorioBase repositorio;

            public Mediador(
                ILogger<Mediador> logger,
                IRepositorioBase repositorio)
            {
                this.logger = logger;
                this.repositorio = repositorio;
            }

            protected override async Task Handle(Comando request, CancellationToken cancellationToken)
            {
                var token = new Random().Next(0, 9999).ToString("D4");
                var certificado = await repositorio.PrimeiroOuPadraoAsync<Certificado>(x => x.Telefone == request.Telefone);

                if(certificado == null)
                {
                    certificado = new Certificado(request.Telefone, token);
                    await repositorio.AdicionarAsync(certificado);
                }
                else
                {
                    certificado.AtualizarToken(token);
                    await repositorio.AtualizarAsync(certificado);
                }

                await Task.Run(() => { Console.WriteLine($"Token enviado: {token}");}, cancellationToken);
            }
        }
    }
}
