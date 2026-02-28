using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Commands
{
    public class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, UsuarioDto>
    {
        private readonly DataContext _context;

        public AtualizarUsuarioCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<UsuarioDto> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (usuario == null)
            {
                throw new Exception("Usuario não encontrado");
            }

            usuario.Nome = request.Nome;
            usuario.Email = request.Email;

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync(cancellationToken);

            return UsuarioDto.From(usuario);
        }
    }
}
