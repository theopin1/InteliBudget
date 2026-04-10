using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Application.Queries.UsuarioQueries;
using IntelliBudgetApi.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries.TransacaoQueries
{
    public class BuscarTransacaoPorIdQueryHandler : IRequestHandler<BuscarTransacaoPorIdQuery, TransacaoDto>
    {
        private readonly DataContext _Context;

        public BuscarTransacaoPorIdQueryHandler(DataContext dataContext)
        {
            _Context = dataContext;
        }

        public async Task<TransacaoDto> Handle(BuscarTransacaoPorIdQuery request, CancellationToken cancellationToken)
        {
            var transacao = await _Context.Transacoes
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (transacao == null)
            {
                throw new Exception("Transação não encontrado");
            }

            return TransacaoDto.From(transacao);
        }
    }
}
