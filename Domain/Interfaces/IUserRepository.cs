using System;
using sobrou.Domain.Entities;

namespace sobrou.Infrastructure.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User?> FindUserByEmail(string email);
    Task<User?> CreateUserAsync(User user);
}
