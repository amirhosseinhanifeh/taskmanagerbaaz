using MsftFramework.Abstractions.CQRS.Command;

namespace MS.Services.TaskCatalog.Contract.Tasks.Commands;

public record UpdateTaskTodayCommand(
            long Id,
            TimeSpan? UserStartTime,
            TimeSpan? UserEndTime,
            string? CEndTime,
            int? AlertRemainder,
            int? UserProgress,
            int? CProgress

    )
    : ITxCreateCommand<FluentResults.Result<bool>>
{
}