using IntelliBudgetApi.Application.Services;
using IntelliBudgetApi.Infra.Data;
using IntelliBudgetApi.Infra.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace IntelliBudgetApi.Application.Commands
{
    public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, LoginResponse>
    {
            private readonly DataContext _context;
            private readonly IPasswordHasher<Usuario> _passwordHasher;
            private readonly ITokenService _tokenService;
            public LoginRequestCommandHandler(DataContext context, IPasswordHasher<Usuario> passwordHasher, ITokenService tokenService)
            {
                _context = context;
                _passwordHasher = passwordHasher;
                _tokenService = tokenService;
            }

            public async Task<LoginResponse> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

                if (usuario == null)
                    throw new Exception("Usuário não encontrado");

                var senhaValida = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, request.Senha);

                if (senhaValida != PasswordVerificationResult.Success)
                {
                throw new Exception("Senha inválida.");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email)
                };

            var token = _tokenService.GerarToken(claims);
                 var refreshToken = _tokenService.GerarRefreshToken();

                 return new LoginResponse
                 {
                    Message = "Login realizado com sucesso",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken
                };
        }
    }
}
