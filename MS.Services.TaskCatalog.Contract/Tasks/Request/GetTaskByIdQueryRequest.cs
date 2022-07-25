using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Contract.Tasks.Request;

public record GetTaskByIdQueryRequest(long Id) : IQuery<FluentResults.Result<GetTaskByIdResult>>;