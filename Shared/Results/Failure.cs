namespace sobrou.Shared.Results;

public sealed record Failure(string Message)
{
    public static Failure UserNotFound => new("User not Found");
    public static Failure PasswordIncorrect => new("Incorrect Password");
    public static Failure IncorrectCredentials => new ("Incorrect email or password.");
}