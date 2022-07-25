using MS.Services.TaskCatalog.Domain.SharedKernel;

namespace MS.Services.TaskCatalog.Contract.Projects.Request;
public record CreateProjectRequest
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = default!;
}