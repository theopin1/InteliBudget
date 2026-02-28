using IntelliBudgetApi.Application.Commands;
using IntelliBudgetApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace IntelliBudgetApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CadastrarUsuarioCommand usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado); ;
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarUsuarioCommand usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado); ;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover([FromBody] DeletarUsuarioCommand usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado); ;
        }

        [HttpGet]
        public async Task<IActionResult> Buscar([FromQuery] BuscarUsuarioPorIdQuery usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado); ;
        }
    }
}
