namespace MS.Services.TaskCatalog.Contract.Users.Dtos;
public record UserDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public string? Avatar { get; init; }
}