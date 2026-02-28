using IntelliBudgetApi.Infra.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands
{
    public class LoginRequestCommand : IRequest<LoginResponse>
    {
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }

}
