using IntelliBudgetApi.Application.Commands.MetaCommands;
using IntelliBudgetApi.Application.Queries.MetaQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntelliBudgetApi.Api.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MetasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MetasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CadastrarMetaCommand usuarioCommand, CancellationToken cancellationToken)
        {
            usuarioCommand.UsuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarMetaCommand usuarioCommand, CancellationToken cancellationToken)
        {
            usuarioCommand.Id = id;
            usuarioCommand.UsuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id, CancellationToken cancellationToken)
        {
            var command = new ExcluirMetaCommand { Id = id };
            var resultado = await _mediator.Send(command, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromQuery] BuscarMetaQuery usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var query = new ListarMetaQuery { UsuarioId = usuarioId };

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
