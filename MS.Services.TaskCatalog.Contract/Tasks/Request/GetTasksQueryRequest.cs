using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Contract.Tasks.Request;

public record GetTasksQueryRequest(
    string? name,
    long[]? projectIds,
    long[]? unitIds,
    long[]? userIds,
    long? controllerId,
    long? testerId,
    long? creatoruserId,
    DateTime? startDate,
    DateTime? endDate,
    priorityType? priorityType,
    TaskSort sort,
    int pageSize,
    int page,
    TodayOrderPriority? todayPriorityOrder
    ) : IQuery<FluentResults.Result<GetTasksResult>>;