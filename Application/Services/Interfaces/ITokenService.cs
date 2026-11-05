using sobrou.Domain.DTOS;

namespace sobrou.Application.Services.Interfaces;

public interface ITokenService
{
    Task<TokenDTO> GenerateJWT(int userId);
}
