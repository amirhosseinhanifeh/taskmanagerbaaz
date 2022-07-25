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
using MS.Services.TaskCatalog.Domain.Projects;

namespace MS.Services.TaskCatalog.Application.Tasks.Features.Commands.Handlers;
public class UpdateTaskOrderHandler : ICommandHandler<UpdateTaskOrderCommand, bool>
{
    private readonly ILogger<CreateTaskHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;

    public UpdateTaskOrderHandler(
        ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateTaskHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }


    public async Task<FluentResults.Result<bool>> Handle(
        UpdateTaskOrderCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

        foreach (var item in command.Ids)
        {
            var created = await _taskCatalogDbContext.Tasks
           .SingleOrDefaultAsync(x => x.Id == item, cancellationToken: cancellationToken);
            if (created != null)
            {
                created.SetOrder(Array.IndexOf(command.Ids, item));
                await _taskCatalogDbContext.SaveChangesAsync();
            }
        }

        var result = new FluentResults.Result();

        return result.ToResult(true);
    }
}