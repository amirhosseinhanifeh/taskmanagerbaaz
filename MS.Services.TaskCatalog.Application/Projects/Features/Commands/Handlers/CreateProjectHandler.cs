using MsftFramework.Abstractions.CQRS.Command;
using MS.Services.TaskCatalog.Contract.Projects.Result;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MsftFramework.Abstractions.Persistence.Mongo;
using MsftFramework.Core.Exception;
using MsftFramework.Core.IdsGenerator;
using AutoMapper;
using MS.Services.TaskCatalog.Infrastructure;
using MS.Services.TaskCatalog.Domain.Projects;
using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Contract.Projects.Commands;
using MS.Services.TaskCatalog.Contract.Projects.Dtos;
using FluentResults;

namespace MS.Services.TaskCatalog.Application.Projects.Features.Commands.Handlers;
public class CreateProjectHandler : ICommandHandler<CreateProjectCommand, CreateProjectResult>
{
    private readonly ILogger<CreateProjectHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _TaskCatalogDbContext;

    public CreateProjectHandler(
        ITaskCatalogDbContext TaskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateProjectHandler> logger)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _TaskCatalogDbContext = Guard.Against.Null(TaskCatalogDbContext, nameof(TaskCatalogDbContext));
    }


    public async Task<Result<CreateProjectResult>> Handle(
        CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));

       // await _domainEventDispatcher.DispatchAsync(cancellationToken, new Events.Domain.CreatingProject());
        var project = Project.Create(
            command.Id,
            command.Name,
            command.Description,
            "فایل");

        await _TaskCatalogDbContext.Projects.AddAsync(project, cancellationToken: cancellationToken);

        await _TaskCatalogDbContext.SaveChangesAsync(cancellationToken);

        //await MongoRepository.AddAsync(project);

        var created = await _TaskCatalogDbContext.Projects
                   .SingleOrDefaultAsync(x => x.Id == project.Id, cancellationToken: cancellationToken);

        var projectDto = _mapper.Map<ProjectDto>(created);

        _logger.LogInformation("Project a with ID: '{ProjectId} created.'", command.Id);

         var result = new FluentResults.Result();

        return result.ToResult( new CreateProjectResult(projectDto));
    }
}