using MsftFramework.Abstractions.CQRS.Command;
using MS.Services.TaskCatalog.Contract.Tasks.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MsftFramework.Abstractions.Persistence.Mongo;
using MsftFramework.Core.Exception;
using MsftFramework.Core.IdsGenerator;
using AutoMapper;
using MS.Services.TaskCatalog.Infrastructure;
using MS.Services.TaskCatalog.Domain.Tasks;
using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Categories.Commands;
using MS.Services.TaskCatalog.Contract.Categories.Result;
using MS.Services.TaskCatalog.Contract.Categories.Dtos;
using MsftFramework.Abstractions.Persistence;
using FluentResults;

namespace MS.Services.TaskCatalog.Application.Categories.Features.Commands.Handlers;
public class CreateCategoryHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
{
    private readonly ILogger<CreateCategoryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;
    public CreateCategoryHandler(
        ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateCategoryHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }


    public async Task<Result<CreateCategoryResult>> Handle(
        CreateCategoryCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        // await _domainEventDispatcher.DispatchAsync(cancellationToken, new Events.Domain.CreatingTask());
        var Category = Domain.Tasks.Category.Create(
            command.Id,
            command.name,
            command.description);

        await _taskCatalogDbContext.Categories.AddAsync(Category, cancellationToken: cancellationToken);

        await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);

        //await MongoRepository.AddAsync(Task);

        var created = await _taskCatalogDbContext.Categories
                   .SingleOrDefaultAsync(x => x.Id == Category.Id, cancellationToken: cancellationToken);

        var CategoryDto = _mapper.Map<CategoryDto>(created);

        _logger.LogInformation("Category a with ID: '{CategoryId} created.'", command.Id);

        var result = new FluentResults.Result();

        return result.ToResult(new CreateCategoryResult(CategoryDto));
    }
}