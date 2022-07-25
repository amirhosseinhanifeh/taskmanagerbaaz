using MsftFramework.Abstractions.CQRS.Command;
using Microsoft.Extensions.Logging;
using MsftFramework.Core.Exception;
using AutoMapper;
using MS.Services.TaskCatalog.Infrastructure;
using Ardalis.GuardClauses;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MediatR;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MsftFramework.Core.IdsGenerator;
using MS.Services.TaskCatalog.Domain.SharedKernel;
using FluentResults;
using MS.Services.TaskCatalog.Contract.Workflows.Result;
using MS.Services.TaskCatalog.Domain.Workflows;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MS.Services.TaskCatalog.Application.Workflows.Features.Commands.Handlers;

public class CreateUserControllerCommandHandler : ICommandHandler<CreateUserControllerCommand, CreateUserControllerResult>
{
    private readonly ILogger<CreateUserControllerCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ITaskCatalogDbContext _taskCatalogDbContext;
    public CreateUserControllerCommandHandler(ITaskCatalogDbContext taskCatalogDbContext,
        IMapper mapper,
        ILogger<CreateUserControllerCommandHandler> logger,
        ICommandProcessor commandProcessor)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _mapper = Guard.Against.Null(mapper, nameof(mapper));
        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
    }

    public async Task<Result<CreateUserControllerResult>> Handle(CreateUserControllerCommand request, CancellationToken cancellationToken)
    {
        var userController = UserController.Create(request.Id, request.userId, request.name, request.order);

        var result = new Result();
        await _taskCatalogDbContext.UserControllers.AddAsync(userController);
        await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);
        var created = await _taskCatalogDbContext.UserControllers
                .FirstOrDefaultAsync(x => x.Id == userController.Id, cancellationToken);
        var TaskDto = _mapper.Map<UserControllerDto>(created);
        return result.ToResult(new CreateUserControllerResult(TaskDto));
    }
}

