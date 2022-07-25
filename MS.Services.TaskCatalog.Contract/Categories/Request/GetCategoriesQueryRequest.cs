using MS.Services.TaskCatalog.Contract.Categories.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Contract.Categories.Request;

public record GetCategoriesQueryRequest() : IQuery<FluentResults.Result<GetCategoriesResult>>;