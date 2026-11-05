namespace sobrou.API.Errors;

public record Error
(
    string Code,
    string Message,
    int Status
);