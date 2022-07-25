using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using MS.Services.TaskCatalog.Contract.Categories.Result;

namespace MS.Services.TaskCatalog.Contract.Categories.Commands;
public record CreateCategoryCommand(
            string name,
            string description
    )
    : ITxCreateCommand<FluentResults.Result<CreateCategoryResult>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}