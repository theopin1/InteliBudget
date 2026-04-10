using IntelliBudgetApi.Infra.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.ContaBancariaCommands
{
    public class DeletarContaBancariaCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
