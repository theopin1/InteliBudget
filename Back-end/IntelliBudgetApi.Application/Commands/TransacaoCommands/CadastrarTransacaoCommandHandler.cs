using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.TransacaoCommands
{
    public class CadastrarTransacaoCommandHandler : IRequestHandler<CadastrarTransacaoCommand, TransacaoDto>
    {
        private readonly DataContext _context;
        public CadastrarTransacaoCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<TransacaoDto> Handle(CadastrarTransacaoCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Exception("Usuario não pode ser vazio");
            }

            var transacao = new Transacao
            {
                Tipo = request.Tipo,
                DataTransacao = request.DataTransacao,
                Valor = request.Valor,
                CategoriaId = request.CategoriaId,
                ContaBancariaId = request.ContaBancariaId
            };

            await _context.Transacoes.AddAsync(transacao, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return TransacaoDto.From(transacao);
        }
    }
}
