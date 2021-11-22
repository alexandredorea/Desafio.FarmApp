using Desafio.Dominio.Mediador.Eventos;
using Desafio.Dominio.Modelos;
using Desafio.Infraestrutura.Dados.Repositorios.Base;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Desafio.Dominio.Mediador.Eventos.EventoUsuarioInserido;

namespace Desafio.Dominio.Mediador.Mediadores.Comandos
{
    public class InserirUsuario 
    {
        public record Comando(string Telefone, string Nome, string Cpf, string Email, string Cep) : IRequest<UsuarioInserido>;

        private class Mediador : IRequestHandler<Comando, UsuarioInserido>
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

            public async Task<UsuarioInserido> Handle(Comando request, CancellationToken cancellationToken)
            {
                var entidade = await repositorio.PrimeiroOuPadraoAsync<Usuario>(x => x.Telefone == request.Telefone || x.Email == request.Email);

                if (entidade != null) 
                {
                    throw new Exception($"Já existe um usuário cadastrado com telefone [{request.Telefone}] ou e-mail [{request.Email}].");
                }
                else
                {
                    entidade = new Usuario(
                        nome: request.Nome,
                        cpf: request.Cpf,
                        telefone: request.Telefone,
                        email: request.Email,
                        cep: request.Cep);
                    await repositorio.AdicionarAsync(entidade);

                    logger.LogInformation($"Usuário Id={entidade.Id} cadastrado com sucesso");
                }

                return new UsuarioInserido { Entidade = entidade };
            }
        }

    }

}
