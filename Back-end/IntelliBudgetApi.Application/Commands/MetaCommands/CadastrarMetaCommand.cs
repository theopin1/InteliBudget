using IntelliBudgetApi.Application.DTO;
using MediatR;


namespace IntelliBudgetApi.Application.Commands.MetaCommands
{
    public class CadastrarMetaCommand : IRequest<MetaDto>
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public decimal ValorAtual { get; set; }
        public decimal ValorAlvo { get; set; }
        public DateTime Prazo { get; set; }
        public int UsuarioId { get; set; }
    }
}
