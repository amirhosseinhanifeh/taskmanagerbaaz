using MS.Services.TaskCatalog.Contract.Projects.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Contract.Projects.Request;

public record GetProjectsQueryRequest(bool? hasSelection) : IQuery<FluentResults.Result<GetProjectResult>>;