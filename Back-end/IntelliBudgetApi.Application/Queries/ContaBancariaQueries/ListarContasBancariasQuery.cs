using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Queries.ContaBancariaQueries
{
    public class ListarContasBancariasQuery : IRequest<List<ContaBancariaDto>>
    {
        public int UsuarioId { get; set; }
    }
}
