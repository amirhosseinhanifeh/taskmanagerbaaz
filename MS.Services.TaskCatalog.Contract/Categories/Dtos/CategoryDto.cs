using MS.Services.TaskCatalog.Domain.SharedKernel;

namespace MS.Services.TaskCatalog.Contract.Categories.Dtos;
public record CategoryDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; private set; } = default!;
}