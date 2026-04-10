using IntelliBudgetApi.Application.Commands.ContaBancariaCommands;
using IntelliBudgetApi.Application.Commands.TransacaoCommands;
using IntelliBudgetApi.Application.Queries.ContaBancariaQueries;
using IntelliBudgetApi.Application.Queries.TransacaoQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasBancariasController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContasBancariasController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CadastrarContaBancariaCommand usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado);
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarContaBancariaCommand usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover([FromBody] DeletarContaBancariaCommand usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Buscar([FromQuery] BuscarContaBancariaPorIdQuery usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ListarContasBancariasQuery usuarioCommand, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado);
        }
    }
}
