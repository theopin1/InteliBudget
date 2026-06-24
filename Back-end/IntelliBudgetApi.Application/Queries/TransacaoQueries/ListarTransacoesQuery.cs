using IntelliBudgetApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries.TransacaoQueries
{
    public class ListarTransacoesQuery : IRequest<List<TransacaoDto>>
    {
        public int UsuarioId { get; set; }
        public int? Quantidade { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
