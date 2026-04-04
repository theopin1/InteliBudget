using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IntelliBudgetApi.Application.Services
{
    public interface ITokenService
    {
        JwtSecurityToken GerarToken(IEnumerable<Claim> claims);
        string GerarRefreshToken();
        ClaimsPrincipal? ValidarToken(string? token);
    }
}
