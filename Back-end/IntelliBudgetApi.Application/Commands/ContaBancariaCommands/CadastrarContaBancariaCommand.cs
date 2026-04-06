using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.ContaBancariaCommands
{
    public class CadastrarContaBancariaCommand : IRequest<ContaBancariaDto>
    {
        public string? NomeBanco { get; set; }
        public string? TipoConta { get; set; }
        public int UsuarioId { get; set; }
        public int Saldo { get; set; }
    }
}
