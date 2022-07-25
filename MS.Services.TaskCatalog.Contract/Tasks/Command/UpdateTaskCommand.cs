using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Tasks.Commands;
public record UpdateTaskCommand(
            TaskName name,
            DateTime? startDateTime,
            DateTime endDateTime,
            long[]? projectIds,
            long[]? unitIds,
            priorityType priority,
            ImportanceType importanceType,
            bool isTodayTask,
            string description,
            long? voiceId,
            long[] userIds,
            long? controllerUserId,
            long? testerUserId,
            TaskDeadLineDto? deadLine,
            string[] requirements,
            long[] imageIds
    )
    : ITxCreateCommand<FluentResults.Result<CreateTaskResult>>
{
    public long Id { get; set; }
}