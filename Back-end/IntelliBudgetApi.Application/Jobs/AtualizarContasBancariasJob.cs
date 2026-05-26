using Azure.Core;
using IntelliBudgetApi.Application.Services;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using IntelliBudgetApi.Infra.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;

namespace IntelliBudgetApi.Application.Jobs
{
    [DisallowConcurrentExecution]
    public class AtualizarContasBancariasJob : IJob
    {
        private readonly ILogger<AtualizarContasBancariasJob> _logger;
        private readonly DataContext _context;
        private readonly PluggyService _pluggyService;

        public AtualizarContasBancariasJob(ILogger<AtualizarContasBancariasJob> logger,
        DataContext context, PluggyService pluggyService)
        {
            _logger = logger;
            _context = context;
            _pluggyService = pluggyService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Iniciando atualização de contas bancárias...");

            var items = await _context.itemBancoUsuarios
                .ToListAsync();

            foreach (var item in items)
            {
                try
                {
                    var accounts = await _pluggyService.GetAccountsAsync(item.ItemId);
                    var account = accounts.FirstOrDefault();
                    if (account == null)
                    {
                        _logger.LogWarning($"Nenhuma conta encontrada para itemId: {item.ItemId}");
                        continue;
                    }

                    var conta = await _context.ContasBancarias
                        .FirstOrDefaultAsync(c => c.UsuarioId == item.UserId);

                    if (conta != null)
                    {
                        conta.Saldo = (int)account.Balance;
                        _context.ContasBancarias.Update(conta);
                    }

                    var from = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
                    var to = DateTime.UtcNow.ToString("yyyy-MM-dd");

                    var transactions = await _pluggyService.GetTransactionsAsync(account.Id, from, to);

                    foreach (var t in transactions)
                    {
                        var existe = await _context.Transacoes
                            .AnyAsync(x => x.DataTransacao == t.Date &&
                                          x.Valor == (int)t.Amount &&
                                          x.ContaBancariaId == conta.Id);

                        if (!existe)
                        {
                            _context.Transacoes.Add(new Transacao
                            {
                                Tipo = t.Amount < 0 ? TipoTransacao.Despesa : TipoTransacao.Receita,
                                DataTransacao = t.Date,
                                Valor = (int)t.Amount,
                                ContaBancariaId = conta.Id,
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Conta do usuário {item.UserId} atualizada com sucesso.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Erro ao atualizar conta do usuário {item.UserId}: {ex.Message}");
                }
            }

            _logger.LogInformation("Atualização de contas bancárias concluída.");
        }
    }

}    

