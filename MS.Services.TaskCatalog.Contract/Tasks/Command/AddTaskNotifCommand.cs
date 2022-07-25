using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Tasks.Commands;
public record AddTaskNotifCommand(
            long taskId,
            TimeSpan time,
            long userId
    )
    : ITxCreateCommand<FluentResults.Result<bool>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}