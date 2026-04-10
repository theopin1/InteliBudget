using IntelliBudgetApi.Application.Commands.TransacaoCommands;
using IntelliBudgetApi.Application.Queries.TransacaoQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntelliBudgetApi.Api.Controllers
{
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

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarTransacaoCommand usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover([FromBody] DeletarTransacaoCommand usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromQuery] BuscarTransacaoPorIdQuery usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado); 
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ListarTransacoesQuery usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado);
        }
    }
}

