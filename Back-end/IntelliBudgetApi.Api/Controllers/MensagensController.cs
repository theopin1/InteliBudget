using IntelliBudgetApi.Application.Commands.ChatBotCommands;
using IntelliBudgetApi.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MensagensController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MensagensController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EnviarMensagem([FromBody] EnviarMensagemRequest request, CancellationToken ct)
        {
            var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new EnviarMensagemCommand(usuarioId, request.Mensagem, request.Historico);

            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }
    }
}
