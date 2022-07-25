//using MsftFramework.Abstractions.CQRS.Command;
//using MS.Services.TaskCatalog.Contract.Tasks.Result;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using MsftFramework.Abstractions.Persistence.Mongo;
//using MsftFramework.Core.Exception;
//using MsftFramework.Core.IdsGenerator;
//using AutoMapper;
//using MS.Services.TaskCatalog.Infrastructure;
//using MS.Services.TaskCatalog.Domain.Tasks;
//using MS.Services.TaskCatalog.Domain.Tasks.ValueObjects;
//using Ardalis.GuardClauses;
//using MS.Services.TaskCatalog.Contract.Tasks.Commands;
//using MS.Services.TaskCatalog.Contract.Tasks.Dtos;

//namespace MS.Services.TaskCatalog.Application.Units.Features.Commands.Handlers;
//public class CreateUnitHandler : ICommandHandler<CreateUnitCommand, CreateUnitResult>
//{
//    private readonly ILogger<CreateTaskHandler> _logger;
//    private readonly IMapper _mapper;
//    private readonly ITaskCatalogDbContext _taskCatalogDbContext;

//    public CreateTaskHandler(
//        ITaskCatalogDbContext taskCatalogDbContext,
//        IMapper mapper,
//        ILogger<CreateTaskHandler> logger)
//    {
//        _logger = Guard.Against.Null(logger, nameof(logger));
//        _mapper = Guard.Against.Null(mapper, nameof(mapper));
//        _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
//    }


//    public async Task<FluentResults.Result<CreateTaskResult>> Handle(
//        CreateTaskCommand command,
//        CancellationToken cancellationToken)
//    {
//        Guard.Against.Null(command, nameof(command));

//        // await _domainEventDispatcher.DispatchAsync(cancellationToken, new Events.Domain.CreatingTask());



//        //var Task = Domain.Tasks.Unit.Create(
//        //    command.Id,
//        //    command.name,
//        //    command.startDateTime,
//        //    command.endDateTime,
//        //    command.projectId,
//        //    command.unitId,
//        //    command.categoryId,
//        //    command.priority,
//        //    command.isTodayTask,
//        //    command.description);

//        //await _taskCatalogDbContext.Tasks.AddAsync(Task, cancellationToken: cancellationToken);

//        //try
//        //{
//        //    await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);

//        //}
//        //catch (Exception e)
//        //{

//        //    throw;
//        //}

//        ////await MongoRepository.AddAsync(Task);

//        //var created = await _taskCatalogDbContext.Tasks
//        //           .SingleOrDefaultAsync(x => x.Id == Task.Id, cancellationToken: cancellationToken);

//        //var TaskDto = _mapper.Map<TaskDto>(created);

//        //_logger.LogInformation("Task a with ID: '{TaskId} created.'", command.Id);

//        var result = new FluentResults.Result();

//        return result.ToResult(new CreateTaskResult(null));
//    }
//}