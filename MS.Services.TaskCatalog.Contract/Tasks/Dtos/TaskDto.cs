using MS.Services.TaskCatalog.Contract.Comments.Dtos;
using MS.Services.TaskCatalog.Domain.SharedKernel;

namespace MS.Services.TaskCatalog.Contract.Tasks.Dtos;
public record TaskDto
{
    public long Id { get; init; }
    public string Name { get; init; } = default!;
    public string Description { get; private set; } = default!;
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public long[]? ProjectIds { get; set; } = default!;
    public long[]? UnitIds { get; set; }
    public long[]? UserIds { get; set; }
    public string? Category { get; set; }
    public priorityType Priority { get; set; }

    public long? VoiceId { get; set; }

    public Domain.SharedKernel.TaskStatus Status { get; set; }
    /// <summary>
    /// اهمیت
    /// </summary>
    public ImportanceType ImportanceType { get; set; }

    /// <summary>
    /// کاربر ایجاد کننده تسک
    /// </summary>
    public string CreatorUserId { get; set; } = string.Empty;

    /// <summary>
    /// کاربر کنترل کننده
    /// </summary>
    public long? ControllerUserId { get; set; }

    /// <summary>
    /// کاربر تستر
    /// </summary>
    public long? TesterUserId { get; set; }

    public TaskDeadLineDto? DeadLine { get; set; }

    public ICollection<CommentDto>? Comments { get; set; }

    public long[]? ImageIds { get; set; }

    public string[] Requirements { get; set; }
}