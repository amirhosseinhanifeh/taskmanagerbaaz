using MS.Services.TaskCatalog.Contract.Users.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Contract.Users.Request;

public record GetUsersQueryRequest(bool? hasSelection) : IQuery<FluentResults.Result<GetUsersResult>>;