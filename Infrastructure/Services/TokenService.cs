using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using sobrou.Application.Services.Interfaces;
using sobrou.Domain.Entities;
using Microsoft.Extensions.Options;
using sobrou.Infrastructure.Config;
using sobrou.Domain.DTOS;

namespace sobrou.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IOptions<SobrouSettings> _settings;
    public TokenService(IOptions<SobrouSettings> settings)
    {
        _settings = settings;
    }
    public async Task<TokenDTO> GenerateJWT(int userId)
    {
        var handler = new JwtSecurityTokenHandler();
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.JwtSecretKey));

        var claims = new []
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
        };

        var jwtToken = new JwtSecurityToken(
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512),
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2)
        );

        TokenDTO token = new()
        {
            Token = handler.WriteToken(jwtToken)
        };

        return token;
    }
}
