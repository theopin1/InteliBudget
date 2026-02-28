using IntelliBudgetApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands
{
    public class CadastrarUsuarioCommand : IRequest<UsuarioDto>
    {
        public string? Nome { get; set; }
        public string? Email { get; set; } = "teste@gmail.com";
        public string? Senha { get; set; }

    }
}
