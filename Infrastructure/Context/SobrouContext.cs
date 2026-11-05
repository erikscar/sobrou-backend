using System;
using Microsoft.EntityFrameworkCore;
using sobrou.Domain.Entities;

namespace sobrou.Infrastructure.Context;

public class SobrouContext(DbContextOptions<SobrouContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}
