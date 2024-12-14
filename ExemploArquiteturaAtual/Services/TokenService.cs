using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExemploArquiteturaAtual.Services;
public static class TokenService
{
    public static string GenerateToken(string email, string name, string signInKey, int validHours)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, name),
            new(ClaimTypes.Email, email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(SecurityKey(signInKey), SecurityAlgorithms.HmacSha256),
            Expires = DateTime.Now.AddHours(validHours)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private static SymmetricSecurityKey SecurityKey(string signinKey)
    {
        var bytes = Encoding.UTF8.GetBytes(signinKey);

        return new SymmetricSecurityKey(bytes);
    }
}

