using IntelliBudgetApi.Application.DTO;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;

namespace IntelliBudgetApi.Application.Commands
{
    public class CadastrarUsuarioCommandHandler : IRequestHandler<CadastrarUsuarioCommand, UsuarioDto>
    {
        private readonly DataContext _context;

        public CadastrarUsuarioCommandHandler(DataContext context)
        {
            _context = context;
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
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return UsuarioDto.From(usuario);
        }
    }

    
}
