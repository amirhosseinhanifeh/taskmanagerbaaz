using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Domain.SharedKernel;

namespace MS.Services.TaskCatalog.Contract.Tasks.Request;
public record CreateTaskRequest
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = default!;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public long[]? ProjectIds { get; set; }
    public long[]? UnitIds { get; set; }
    public priorityType Priority { get; set; }
    public ImportanceType ImportanceType { get; set; }
    public long[]? UserIds { get; set; }
    public long? ControllerUserId { get; set; }
    public long? TesterUserId { get; set; }
    public long? VoiceId { get; set; }
    public string[]? Requirements { get; set; }
    public TaskDeadLineDto? DeadLine { get; set; }
    public long[]? ImageIds { get; set; }
}