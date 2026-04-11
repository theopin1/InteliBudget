using IntelliBudgetApi.Application.Commands.ContaBancariaCommands;
using IntelliBudgetApi.Application.Commands.ItemBancoUsuarioCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IntelliBudgetApi.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ItemBancoUsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ItemBancoUsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CadastrarItemBancoUsuarioCommand usuarioCommand, CancellationToken cancellationToken)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            usuarioCommand.UserId = int.Parse(userIdString!);

            var resultado = await _mediator.Send(usuarioCommand, cancellationToken);
            return Ok(resultado);
        }

        [HttpPost("sync")]
        public async Task<IActionResult> AdicionarSync([FromBody] SyncContaCommand syncCommand, CancellationToken cancellationToken)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized();

            syncCommand.UserId = int.Parse(userIdString);

            var resultado = await _mediator.Send(syncCommand, cancellationToken);
            return Ok(resultado);
        }
    }
}
