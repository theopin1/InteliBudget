using IntelliBudgetApi.Application.Services;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using IntelliBudgetApi.Infra.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.ItemBancoUsuarioCommands
{
    public class SyncContaCommandHandler : IRequestHandler<SyncContaCommand, Unit>
    {
        private readonly DataContext _context;
        private readonly PluggyService _pluggyService;

        public SyncContaCommandHandler(DataContext context, PluggyService pluggyService)
        {
            _context = context;
            _pluggyService = pluggyService;
        }

        public async Task<Unit> Handle(SyncContaCommand request, CancellationToken cancellationToken)
        {
            var from = DateTime.UtcNow.AddMonths(-3).ToString("yyyy-MM-dd");
            var to = DateTime.UtcNow.ToString("yyyy-MM-dd");

            var accounts = await _pluggyService.GetAccountsAsync(request.ItemId);
            var account = accounts.FirstOrDefault();

            if (account == null) return Unit.Value;

            var conta = new ContaBancaria
            {
                NomeBanco = account.Name,
                TipoConta = account.Type,
                Saldo = (int)account.Balance,
                UsuarioId = request.UserId,
            };
            _context.ContasBancarias.Add(conta);
            await _context.SaveChangesAsync(cancellationToken);

            var transactions = await _pluggyService.GetTransactionsAsync(account.Id, from, to);
            foreach (var t in transactions)
            {
                _context.Transacoes.Add(new Transacao
                {
                    Tipo = t.Amount < 0 ? TipoTransacao.Despesa : TipoTransacao.Receita,
                    DataTransacao = t.Date,
                    Valor = (int)t.Amount,
                    ContaBancariaId = conta.Id,
                });
            }
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
