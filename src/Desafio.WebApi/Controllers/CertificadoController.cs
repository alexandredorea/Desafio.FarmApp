using Desafio.Dominio.Mediador.Mediadores.Consultas;
using Desafio.Dominio.Modelos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Desafio.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CertificadoController : Controller
    {
        private readonly IMediator mediador;

        public CertificadoController(IMediator mediador)
        {
            this.mediador = mediador;
        }

        [HttpGet("validar/{token}")]
        [ProducesResponseType(typeof(Certificado), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute][Required] string token)
        {
            var retorno = await mediador.Send(new VerificarCertificado.Consulta(token));

            if (retorno == null)
                return NotFound();
            else
                return Ok(retorno);
        }
    }
}
