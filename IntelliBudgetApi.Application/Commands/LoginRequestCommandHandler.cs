using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace IntelliBudgetApi.Application.Commands
{
    public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, LoginResponse>
    {
            private readonly DataContext _context;

            public LoginRequestCommandHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<LoginResponse> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado");

                if (usuario.Senha != request.Senha)
                    throw new Exception("Senha inválida");

                return new LoginResponse
                {
                    Message = "Login realizado com sucesso"
                };
            }
    }
}
