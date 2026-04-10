using IntelliBudgetApi.Application.DTO;
using MediatR;

namespace IntelliBudgetApi.Application.Commands.ContaBancariaCommands
{
    public class AtualizarContaBancariaCommand : IRequest<ContaBancariaDto>
    {
        public int Id { get; set; }
        public string? NomeBanco { get; set; }
        public string? TipoConta { get; set; }
        public int UsuarioId { get; set; }
        public int Saldo { get; set; }
    }
}
