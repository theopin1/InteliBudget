using IntelliBudgetApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries.MetaQueries
{
    public class ListarMetaQuery : IRequest<List<MetaDto>>
    {
        public int UsuarioId { get; set; }
    }
}
