using IntelliBudgetApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.LoginCommands
{
    public class LoginRequestCommand : IRequest<LoginResponseDto>
    {
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }

}
