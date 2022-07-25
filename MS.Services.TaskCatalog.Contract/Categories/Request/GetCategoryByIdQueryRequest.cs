using FluentResults;
using MS.Services.TaskCatalog.Contract.Categories.Result;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Contract.Categories.Request;

public record GetCategoryByIdQueryRequest(long Id) : IQuery<Result<GetCategoryByIdResult>>;