using IntelliBudgetApi.Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.MetaCommands
{
    public class AtualizarMetaCommand : IRequest<MetaDto>
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal ValorAtual { get; set; }
        public decimal ValorAlvo { get; set; }
        public DateTime Prazo { get; set; }
        public int UsuarioId { get; set; }
    }
}
