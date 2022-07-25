using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Contract.Workflows.Request;

public record GetWorkflowStepsByIdQueryRequest(long Id) : IQuery<FluentResults.Result<GetWorkflowStepsResult>>;

public record GetWorkflowStepsQueryRequest() : IQuery<FluentResults.Result<GetWorkflowStepsResult>>;