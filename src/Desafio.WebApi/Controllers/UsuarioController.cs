using Desafio.Dominio.Mediador.Mediadores.Comandos;
using Desafio.Dominio.Mediador.Mediadores.Consultas;
using Desafio.Dominio.Modelos;
using Desafio.WebApi.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Desafio.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator mediador;

        public UsuarioController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        [HttpGet("buscar/{id}", Name = "buscar")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Buscar([FromRoute][Required] Guid id)
        {
            var retorno = await mediador.Send(new VerificarUsuario.ConsultaPorId(id));

            if (retorno == null)
                return NotFound();
            else
                return Ok(retorno);
        }

        [HttpGet("validar/{telefone}")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Validar([FromRoute][Required] string telefone)
        {
            var retorno = await mediador.Send(new VerificarUsuario.ConsultaPorTelefone(telefone));

            if (retorno == null)
                return Accepted();
            else
                return Ok(retorno);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] UsuarioViewModel viewModel)
        {
            var comando = new InserirUsuario.Comando(viewModel.Telefone, viewModel.Nome, viewModel.Cpf, viewModel.Email, viewModel.Cep);
            var retorno = await mediador.Send(comando);
            return CreatedAtRoute("buscar", new { id = retorno.Entidade.Id }, retorno.Entidade);
        }
    }
}