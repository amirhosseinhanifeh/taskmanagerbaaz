namespace MS.Services.TaskCatalog.Contract.Users.Request;
public record CreateUserRequest
{
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
}