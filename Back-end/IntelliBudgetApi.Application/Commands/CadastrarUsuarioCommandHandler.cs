using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace IntelliBudgetApi.Application.Commands
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, UsuarioDto>
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public CadastrarUsuarioCommandHandler(DataContext context, IPasswordHasher<Usuario> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<UsuarioDto> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (request == null) 
                throw new Exception("Usuario não pode ser vazio");

            var usuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = request.Senha, 
            };

            usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return UsuarioDto.From(usuario);
        }
    }

    
}
