using System.Security.Claims;
using Azure;
using sobrou.Application.Services.Interfaces;
using sobrou.Domain.DTOS;
using sobrou.Domain.Entities;
using sobrou.Infrastructure.Repositories.Interfaces;
using sobrou.Shared.Results;

namespace sobrou.Application.Services;

public class UserService(
    IUserRepository userRepository, 
    IPasswordService passwordService,
    ITokenService tokenService
    ) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordService _passwordService = passwordService; 
    private readonly ITokenService _tokenService = tokenService;

    public async Task<List<User>> GetUsersAsync()
    {
        var users = await _userRepository.GetUsersAsync();
        
        return users;
    }

    public async Task<Result<TokenDTO>> LoginUserAsync(LoginRequestDTO userData)
    {
        User user = await _userRepository.FindUserByEmail(userData.Email);

        if(user != null)
        {
            bool isPasswordCorrect = _passwordService.Verify(userData.Password, user.PasswordHash);

            if (user is null || !isPasswordCorrect) 
            {
                return Result<TokenDTO>.Failure(Failure.IncorrectCredentials);
            }
        }
        else
        {
            return Result<TokenDTO>.Failure(Failure.IncorrectCredentials);
        }


        TokenDTO token = await _tokenService.GenerateJWT(user.Id);

        return Result<TokenDTO>.Success(token);
    }

    public async Task<Result<TokenDTO>> RegisterUserAsync(RegisterRequestDTO userData)
    {
        string password = _passwordService.Hash(userData.Password);

        User user = new(userData.FirstName, userData.LastName, userData.Email, password);

        var createdUser = await _userRepository.CreateUserAsync(user);

        if (createdUser is null)
        {
            return Result<TokenDTO>.Failure(Failure.UserNotFound);
        }

        TokenDTO token = await _tokenService.GenerateJWT(createdUser.Id);

        return Result<TokenDTO>.Success(token);
    }
}
