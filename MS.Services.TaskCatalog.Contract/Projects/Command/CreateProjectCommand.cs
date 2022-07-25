using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Contract.Projects.Result;
using MS.Services.TaskCatalog.Domain.Projects.ValueObjects;

namespace MS.Services.TaskCatalog.Contract.Projects.Commands;
public record CreateProjectCommand(
            ProjectName Name,
            string Description
    )
    : ITxCreateCommand<FluentResults.Result<CreateProjectResult>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}