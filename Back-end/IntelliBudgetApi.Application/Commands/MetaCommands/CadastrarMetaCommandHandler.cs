using Azure.Core;
using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.MetaCommands
{
    public class CadastrarMetaCommandHandler : IRequestHandler<CadastrarMetaCommand, MetaDto>
    {
        private readonly DataContext _context;

        public CadastrarMetaCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<MetaDto> Handle(CadastrarMetaCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new Exception("Meta não pode ser vazia");
            }

            var meta = new Meta
            {
                UsuarioId = request.UsuarioId,
                Nome = request.Nome,
                Descricao = request.Descricao,
                ValorAlvo = request.ValorAlvo,
                ValorAtual = request.ValorAtual,
                Prazo = request.Prazo
            };

            await _context.AddAsync(meta, cancellationToken);
            await _context.SaveChangesAsync();

            return MetaDto.from(meta);
        }

    }
}
