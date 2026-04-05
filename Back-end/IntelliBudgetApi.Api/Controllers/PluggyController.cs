using IntelliBudgetApi.Application.Queries.PluggyQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PluggyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PluggyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("connect-token")]
        public async Task<IActionResult> GetConnectToken()
        {
            var response = await _mediator.Send(new BuscarPluggyAccessTokenQuery());
            return Ok(response);
        }
    }
}
