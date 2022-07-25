using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Contract.Tasks.Request;

public record GetTaskProgressQueryRequest(long id) : IQuery<FluentResults.Result<GetTaskProgressResult>>;