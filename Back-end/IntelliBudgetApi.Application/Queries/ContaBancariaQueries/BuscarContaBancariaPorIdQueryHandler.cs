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
    public class BuscarContaBancariaPorIdQueryHandler : IRequestHandler<BuscarContaBancariaPorIdQuery, ContaBancariaDto>
    {
        private readonly DataContext _Context;

        public BuscarContaBancariaPorIdQueryHandler(DataContext dataContext)
        {
            _Context = dataContext;
        }

        public async Task<ContaBancariaDto> Handle(BuscarContaBancariaPorIdQuery request, CancellationToken cancellationToken)
        {
            var contaBancaria = await _Context.ContasBancarias
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (contaBancaria == null)
            {
                throw new Exception("Conta não encontrado");
            }

            return ContaBancariaDto.From(contaBancaria);
        }
    }
}
