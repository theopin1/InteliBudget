using IntelliBudgetApi.Application.Commands.TransacaoCommands;
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
    public class DeletarContaBancariaCommandHandler : IRequestHandler<DeletarContaBancariaCommand, bool>
    {
        private readonly DataContext _context;

        public DeletarContaBancariaCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeletarContaBancariaCommand request, CancellationToken cancellationToken)
        {
            var contaBancaria = await _context.ContasBancarias
               .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (contaBancaria == null)
            {
                throw new Exception("Conta não encontrado");
            }

            _context.ContasBancarias.Remove(contaBancaria);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
