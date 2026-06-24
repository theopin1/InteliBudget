using IntelliBudgetApi.Application.DTO;
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
    public class ListarTransacoesQueryHandler : IRequestHandler<ListarTransacoesQuery, List<TransacaoDto>>
    {
        private readonly DataContext _Context;

        public ListarTransacoesQueryHandler(DataContext dataContext)
        {
            _Context = dataContext;
        }

        public async Task<List<TransacaoDto>> Handle(ListarTransacoesQuery request, CancellationToken cancellationToken)
        {


            var query = _Context.Transacoes
                .Include(x => x.Categoria)
                .Include(x => x.ContaBancaria)
                .Where(x => x.ContaBancaria.UsuarioId == request.UsuarioId);

            if (request.DataInicio.HasValue)
            {
                query = query.Where(x => x.DataTransacao >= request.DataInicio.Value);
            }

            if (request.DataFim.HasValue)
            {
                query = query.Where(x => x.DataTransacao <= request.DataFim.Value);
            }

            query = query.OrderByDescending(x => x.DataTransacao);

            if (request.Quantidade.HasValue)
            {
                query = query.Take(request.Quantidade.Value);
            }

            var transacoes = await query
                .Select(x => TransacaoDto.From(x))
                .ToListAsync(cancellationToken);

            return transacoes;
        }
    }
}
