using MS.Services.TaskCatalog.Contract.Comments.Result;
using MsftFramework.Abstractions.CQRS.Query;

namespace MS.Services.TaskCatalog.Contract.Comments.Request;

public record GetCommentByIdQueryRequest(long Id) : IQuery<FluentResults.Result<GetCommentByIdResult>>;