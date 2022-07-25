using FluentResults;
using MS.Services.TaskCatalog.Contract.workflows.Result;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Contract.Workflows.Request;
public record GetWorkflowByIdQueryRequest(long Id) : IQuery<FluentResults.Result<GetWorkflowByIdResult>>;

public record GetWorkflowQueryRequest() : IQuery<FluentResults.Result<GetWorkflowResult>>;
public record GetWorkflowInstanceQueryRequest(long? workflowId) : IQuery<FluentResults.Result<GetWorkflowInstanceResult>>;

public record GetWorkflowAlertQueryRequest() : IQuery<FluentResults.Result<GetWorkflowAlertsResult>>;
