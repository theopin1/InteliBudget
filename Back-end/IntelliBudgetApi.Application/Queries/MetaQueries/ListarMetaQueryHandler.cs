using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Application.Queries.ContaBancariaQueries;
using IntelliBudgetApi.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries.MetaQueries
{
    public class ListarMetaQueryHandler : IRequestHandler<ListarMetaQuery, List<MetaDto>>
    {
        private readonly DataContext _Context;

        public ListarMetaQueryHandler(DataContext dataContext)
        {
            _Context = dataContext;
        }

        public async Task<List<MetaDto>> Handle(ListarMetaQuery request, CancellationToken cancellationToken)
        {
            var metas = await _Context.metas
                .Where(x => x.UsuarioId == request.UsuarioId)
                .Select(x => MetaDto.from(x))
                .ToListAsync(cancellationToken);

            return metas;
        }
    }
}
