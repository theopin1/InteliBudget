using IntelliBudgetApi.Application.Commands.ContaBancariaCommands;
using IntelliBudgetApi.Application.Commands.TransacaoCommands;
using IntelliBudgetApi.Application.Queries.ContaBancariaQueries;
using IntelliBudgetApi.Application.Queries.TransacaoQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IntelliBudgetApi.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TransacoesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CadastrarTransacaoCommand usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarTransacaoCommand usuarioCommand, CancellationToken cancellationToken)
        {
            usuarioCommand.Id = id;
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id, CancellationToken cancellationToken)
        {
            var command = new DeletarTransacaoCommand { Id = id };
            var resultado = await _mediator.Send(command, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromQuery] BuscarTransacaoPorIdQuery usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado); 
        }

        [HttpGet]
        public async Task<IActionResult> Listar(CancellationToken cancellationToken)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var query = new ListarTransacoesQuery { UsuarioId = usuarioId };

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}

