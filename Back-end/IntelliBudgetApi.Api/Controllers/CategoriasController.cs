using IntelliBudgetApi.Application.Commands.TransacaoCommands;
using IntelliBudgetApi.Application.Queries.CategoriaQueries;
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
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ListarCategoriasQuery usuarioQuery, CancellationToken cancellationToken)
        {
            var resultado = await _mediator.Send(usuarioQuery, cancellationToken);
            return Ok(resultado);
        }
    }
}
