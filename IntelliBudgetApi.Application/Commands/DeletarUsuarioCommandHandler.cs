using IntelliBudgetApi.Infra.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands
{
    public class DeletarUsuarioCommandHandler : IRequestHandler<DeletarUsuarioCommand, bool>
    {
        private readonly DataContext _context;

        public DeletarUsuarioCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeletarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (usuario == null)
            {
                throw new Exception("Usuario não encontrado");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
