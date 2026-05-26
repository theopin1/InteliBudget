using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries.CategoriaQueries
{
    public class ListarCategoriasQueryHandler : IRequestHandler<ListarCategoriasQuery, List<CategoriaDto>>
    {
        private readonly DataContext _context;

        public ListarCategoriasQueryHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaDto>> Handle(ListarCategoriasQuery query, CancellationToken cancellationToken)
        {
            var categorias = await _context.Categorias
                .Select(x => CategoriaDto.From(x))
                .ToListAsync(cancellationToken);

            return categorias;
        }
    }
}
