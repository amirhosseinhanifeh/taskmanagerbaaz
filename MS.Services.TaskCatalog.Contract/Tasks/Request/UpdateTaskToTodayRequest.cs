using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;

namespace MS.Services.TaskCatalog.Contract.Tasks.Request;
public record UpdateTaskToTodayRequest
{
    public long Id { get; init; }
    public bool IsToday { get; set; }
}
public record UpdateTaskTodayRequest
{
    public long Id { get; set; }
    public TimeSpan? UStartTime { get; set; }
    public TimeSpan? UEndTime { get; set; }
    public string? CEndTime { get; set; }
    public int? AlertRemainder { get; set; }
    public int? UserProgress { get; set; }
    public int? CProgress { get; set; }
}