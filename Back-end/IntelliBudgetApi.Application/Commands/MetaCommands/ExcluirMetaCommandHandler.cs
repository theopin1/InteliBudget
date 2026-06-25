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
    public class ExcluirMetaCommandHandler : IRequestHandler<ExcluirMetaCommand, bool>
    {
        private readonly DataContext _context;
        public ExcluirMetaCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ExcluirMetaCommand request, CancellationToken cancellationToken)
        {
            var meta = await _context.metas
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (meta == null) {
                throw new Exception("Meta não encontrada");
            }

            _context.metas.Remove(meta);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}                       
