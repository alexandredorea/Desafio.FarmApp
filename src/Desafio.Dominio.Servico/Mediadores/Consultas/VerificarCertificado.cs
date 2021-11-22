using Desafio.Dominio.Modelos;
using Desafio.Infraestrutura.Dados.Repositorios.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.Dominio.Mediador.Mediadores.Consultas
{
    public class VerificarCertificado
    {
        public record Consulta(string Token) : IRequest<Certificado>;

        private class Mediador : IRequestHandler<Consulta, Certificado>
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

            public async Task<Certificado> Handle(Consulta request, CancellationToken cancellationToken)
            {
                var certificado = await repositorio.PrimeiroOuPadraoAsync<Certificado>(x => x.Token == request.Token);
                if (certificado == null)
                {
                    _ = Task.Run(() => { Console.WriteLine($"Token {request.Token} é inválido"); });
                    return null;
                }

                certificado.ValidarToken(true);
                await repositorio.AtualizarAsync(certificado);

                return certificado;
            }
        }
    }
}
