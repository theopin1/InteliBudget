using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Application.Queries.TransacaoQueries;
using IntelliBudgetApi.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries.ContaBancariaQueries
{
    public class ListarContasBancariasQueryHandler : IRequestHandler<ListarContasBancariasQuery, List<ContaBancariaDto>>
    {
        private readonly DataContext _Context;

        public ListarContasBancariasQueryHandler(DataContext dataContext)
        {
            _Context = dataContext;
        }

        public async Task<List<ContaBancariaDto>> Handle(ListarContasBancariasQuery request, CancellationToken cancellationToken)
        {
            var transacoes = await _Context.ContasBancarias
                .Where(x => x.UsuarioId == request.UsuarioId)
                .Select(x => ContaBancariaDto.From(x))
                .ToListAsync(cancellationToken);



            return transacoes;
        }
    }
    
}
