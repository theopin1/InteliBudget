using IntelliBudgetApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries.TransacaoQueries
{
    public class BuscarTransacaoPorIdQuery : IRequest<TransacaoDto>
    {
        public int Id { get; set; }
    }
}
