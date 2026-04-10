using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.TransacaoCommands
{
    public class DeletarTransacaoCommandHandler : IRequestHandler<DeletarTransacaoCommand, bool>
    {
        private readonly DataContext _context;

        public DeletarTransacaoCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeletarTransacaoCommand request, CancellationToken cancellationToken)
        {
            var transacao = await _context.Transacoes
               .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (transacao == null)
            {
                throw new Exception("Transação não encontrado");
            }

            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
