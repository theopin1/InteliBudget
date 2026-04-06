using FluentValidation;
using IntelliBudgetApi.Application.Commands.TransacaoCommands;
using IntelliBudgetApi.Infra.Data;
using Microsoft.EntityFrameworkCore;

public class CadastrarTransacaoCommandValidator : AbstractValidator<CadastrarTransacaoCommand>
{
    private readonly DataContext _context;

    public CadastrarTransacaoCommandValidator(DataContext context)
    {
        _context = context;

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("O valor da transação deve ser maior que zero.");

        RuleFor(x => x.CategoriaId)
            .MustAsync(CategoriaExiste)
            .WithMessage("A categoria informada não existe.");

        RuleFor(x => x.ContaBancariaId)
            .MustAsync(ContaExiste)
            .WithMessage("A conta bancária informada não existe.");
    }

    private async Task<bool> CategoriaExiste(int categoriaId, CancellationToken cancellationToken)
    {
        return await _context.Categorias
            .AnyAsync(x => x.Id == categoriaId, cancellationToken);
    }

    private async Task<bool> ContaExiste(int contaId, CancellationToken cancellationToken)
    {
        return await _context.ContasBancarias
            .AnyAsync(x => x.Id == contaId, cancellationToken);
    }
}