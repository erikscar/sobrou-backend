namespace sobrou.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public string? PasswordHash { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set;}

    public User() {}
    public User(string firstName, string lastName, string email, string passwordHash)
    {
        Name = $"{firstName.Trim()} {lastName.Trim()}";
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
