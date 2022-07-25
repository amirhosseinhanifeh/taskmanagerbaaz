using MS.Services.TaskCatalog.Contract.Users.Result;
using MS.Services.TaskCatalog.Contract.Users.Request;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Users.Commands;
public record CreateUserCommand(
    string Name) : ITxCreateCommand<FluentResults.Result<GetUsersResult>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}