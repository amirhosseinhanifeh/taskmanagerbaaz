using MS.Services.TaskCatalog.Domain.SharedKernel;

namespace MS.Services.TaskCatalog.Contract.Tasks.Dtos;
public record TasksDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public string CreateDate { get; set; } = default!;
    public int ControllerProgress { get; set; }
    public int UserProgress { get; set; }
    public long CreatorUserId { get; set; }
    public int ReturnCount { get; set; }
    public bool DragLocked { get; set; }
}

public record TodayTasksDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public string CreateDate { get; set; } = default!;
    public int ControllerProgress { get; set; }
    public int UserProgress { get; set; }
    public long CreatorUserId { get; set; }
    public int ReturnCount { get; set; }
    public bool DragLocked { get; set; }
    public bool HasController { get; set; }
    public bool HasUser { get; set; }
    /// <summary>
    /// پایان کنترلر
    /// </summary>
    public string? ControllerEndTime { get; set; }
    public string? UserEndTime { get;  set; }
    public string? CreatorStart { get; set; }


    /// <summary>
    /// سیستم یادآور
    /// </summary>
    public int? AlartReminder { get; set; }
}