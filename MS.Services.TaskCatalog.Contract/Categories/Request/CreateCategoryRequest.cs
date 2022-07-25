using MS.Services.TaskCatalog.Domain.SharedKernel;

namespace MS.Services.TaskCatalog.Contract.Categories.Request;
public record CreateCategoryRequest
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = default!;
}