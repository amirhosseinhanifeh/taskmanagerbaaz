using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;

namespace MS.Services.TaskCatalog.Contract.Tasks.Request;
public record UpdateTaskOrderRequest
{

    public long[] Ids { get; init; } = null!;
}