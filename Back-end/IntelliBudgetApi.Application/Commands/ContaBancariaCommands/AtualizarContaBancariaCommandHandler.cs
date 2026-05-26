using IntelliBudgetApi.Application.Commands.TransacaoCommands;
using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.ContaBancariaCommands
{
    public class AtualizarContaBancariaCommandHandler : IRequestHandler<AtualizarContaBancariaCommand, ContaBancariaDto>
    {
        private readonly DataContext _context;

        public AtualizarContaBancariaCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<ContaBancariaDto> Handle(AtualizarContaBancariaCommand request, CancellationToken cancellationToken)
        {
            var contaBancaria = await _context.ContasBancarias
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (contaBancaria == null)
            {
                throw new Exception("Conta não encontrado");
            }

            contaBancaria.Id = request.Id;
            contaBancaria.NomeBanco = request.NomeBanco;
            contaBancaria.TipoConta = request.TipoConta;
            contaBancaria.UsuarioId = 7;
            contaBancaria.Saldo = request.Saldo;

            _context.ContasBancarias.Update(contaBancaria);
            await _context.SaveChangesAsync(cancellationToken);

            return ContaBancariaDto.From(contaBancaria);
        }
    }
}
