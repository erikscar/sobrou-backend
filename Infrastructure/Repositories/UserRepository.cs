using System;
using Microsoft.EntityFrameworkCore;
using sobrou.Domain.Entities;
using sobrou.Infrastructure.Context;
using sobrou.Infrastructure.Repositories.Interfaces;

namespace sobrou.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private SobrouContext _sobrouContext;

    public UserRepository(SobrouContext sobrouContext)
    {
        _sobrouContext = sobrouContext;
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _sobrouContext.Users.ToListAsync();
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        return await _sobrouContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        await _sobrouContext.Users.AddAsync(user);

        await _sobrouContext.SaveChangesAsync();

        return user;
    }
}
