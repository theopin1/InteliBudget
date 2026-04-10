using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.TransacaoCommands
{
    public class DeletarTransacaoCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
