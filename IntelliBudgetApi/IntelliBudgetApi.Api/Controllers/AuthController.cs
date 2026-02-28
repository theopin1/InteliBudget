using IntelliBudgetApi.Application.Commands;
using IntelliBudgetApi.Infra.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
    
namespace IntelliBudgetApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
