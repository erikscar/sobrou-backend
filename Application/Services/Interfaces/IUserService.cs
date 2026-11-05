using System;
using sobrou.Domain.DTOS;
using sobrou.Domain.Entities;
using sobrou.Shared.Results;

namespace sobrou.Application.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<Result<TokenDTO>> LoginUserAsync(LoginRequestDTO userData);
    Task<Result<TokenDTO>> RegisterUserAsync(RegisterRequestDTO userData);
}
