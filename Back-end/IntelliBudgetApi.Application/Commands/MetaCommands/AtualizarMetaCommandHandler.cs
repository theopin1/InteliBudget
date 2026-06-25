using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands.MetaCommands
{
    public class AtualizarMetaCommandHandler : IRequestHandler<AtualizarMetaCommand, MetaDto>
    {
        private readonly DataContext _context;

        public AtualizarMetaCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<MetaDto> Handle(AtualizarMetaCommand request, CancellationToken cancellationToken)
        {
            var meta = await _context.metas
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (meta == null)
            {
                throw new Exception("Meta não encontrada");
            }

            meta.Id = request.Id;
            meta.Nome = request.Nome;
            meta.Descricao = request.Descricao;
            meta.ValorAlvo = request.ValorAlvo;
            meta.ValorAtual = request.ValorAtual;
            meta.Prazo = request.Prazo; 
            meta.UsuarioId = request.UsuarioId;

            _context.metas.Update(meta);
            await _context.SaveChangesAsync();

            return MetaDto.from(meta);
        }
    }
}
