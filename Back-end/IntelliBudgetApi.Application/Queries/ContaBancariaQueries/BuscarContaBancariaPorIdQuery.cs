using IntelliBudgetApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries.ContaBancariaQueries
{
    public class BuscarContaBancariaPorIdQuery : IRequest<ContaBancariaDto>
    {
        public int Id { get; set; }
    }
}
