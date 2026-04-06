using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.TransacaoCommands
{
    public class AtualizarTransacaoCommandHandler : IRequestHandler<AtualizarTransacaoCommand, TransacaoDto>
    {
        private readonly DataContext _context;

        public AtualizarTransacaoCommandHandler(DataContext context)
        {
            _context = context; 
        }

        public async Task<TransacaoDto> Handle(AtualizarTransacaoCommand request, CancellationToken cancellationToken)
        {
            var transacao = await _context.Transacoes
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (transacao == null)
            {
                throw new Exception("Transação não encontrado");
            }

            transacao.Id = request.Id;
            transacao.Tipo = request.Tipo;
            transacao.DataTransacao = request.DataTransacao;
            transacao.Valor = request.Valor;
            transacao.CategoriaId = request.CategoriaId;
            transacao.ContaBancariaId = request.ContaBancariaId;

            _context.Transacoes.Update(transacao);
            await _context.SaveChangesAsync(cancellationToken);

            return TransacaoDto.From(transacao);
        }
    }
}
