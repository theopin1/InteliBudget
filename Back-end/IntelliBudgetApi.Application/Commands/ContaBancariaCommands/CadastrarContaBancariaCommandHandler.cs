using IntelliBudgetApi.Application.Commands.TransacaoCommands;
using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.ContaBancariaCommands
{
    internal class CadastrarContaBancariaCommandHandler : IRequestHandler<CadastrarContaBancariaCommand, ContaBancariaDto>
    {
        private readonly DataContext _context;
        public CadastrarContaBancariaCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<ContaBancariaDto> Handle(CadastrarContaBancariaCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Exception("Conta não pode ser vazio");
            }

            var contaBancaria = new ContaBancaria
            {
                NomeBanco = request.NomeBanco,
                TipoConta = request.TipoConta,
                UsuarioId = request.UsuarioId,
                Saldo = request.Saldo
            };

            await _context.ContasBancarias.AddAsync(contaBancaria, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return ContaBancariaDto.From(contaBancaria);
        }
    }
}
