using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Abstractions.CQRS.Query;
using MsftFramework.Core.IdsGenerator;

namespace MS.Services.TaskCatalog.Contract.Workflows.Request;

public record GetWorkflowRoleModelRequest(long? UnitId) : IQuery<FluentResults.Result<GetWorkflowRoleModelResult>>;


