namespace sobrou.Infrastructure.Repositories.Interfaces;

public interface IPasswordService
{
    public string Hash(string password);
    public bool Verify(string password, string passwordHash);
}