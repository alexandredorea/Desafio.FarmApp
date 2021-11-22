using Desafio.Dominio.Modelos;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Desafio.Dominio.Mediador.Eventos
{
    public class EventoUsuarioInserido
    {
        public class UsuarioInserido : INotification
        {
            public Usuario Entidade { get; set; }

            public bool CadastroFinalizado { get; internal set; }
        }

        private class Mediador : INotificationHandler<UsuarioInserido>
        {
            private readonly ILogger<Mediador> logger;

            public Mediador(ILogger<Mediador> logger)
            {
                this.logger = logger;
            }

            public async Task Handle(UsuarioInserido notification, CancellationToken cancellationToken)
            {
                if (notification.CadastroFinalizado)
                    return;

                try
                {
                    await Task.Run(() => { Console.WriteLine($"Disparado e-mail para: {notification.Entidade.Email}"); }, cancellationToken);
                    notification.CadastroFinalizado = true;
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Erro: Ignorando processamento...");
                }
            }
        }
    }
}
