using Ardalis.GuardClauses;
using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MS.Services.TaskCatalog.Contract.Users.Command;
using MS.Services.TaskCatalog.Contract.Users.Dtos;
using MS.Services.TaskCatalog.Contract.Users.Result;
using MS.Services.TaskCatalog.Domain.Users;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Application.Users.Features.Commands.Handlers
{
    public class AddUserToSelectionHandler : ICommandHandler<AddUserToSelectionCommand, AddUserToSelectionResult>
    {
        private readonly ILogger<AddUserToSelectionHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITaskCatalogDbContext _taskCatalogDbContext;
        public AddUserToSelectionHandler(
            ITaskCatalogDbContext taskCatalogDbContext,
            IMapper mapper,
            ILogger<AddUserToSelectionHandler> logger)
        {
            _logger = Guard.Against.Null(logger, nameof(logger));
            _mapper = Guard.Against.Null(mapper, nameof(mapper));
            _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
        }


        public async Task<Result<AddUserToSelectionResult>> Handle(
            AddUserToSelectionCommand command,
            CancellationToken cancellationToken)
        {
            Guard.Against.Null(command, nameof(command));

            // await _domainEventDispatcher.DispatchAsync(cancellationToken, new Events.Domain.CreatingTask());
            var userSelection = UserSelection.Create(
                command.Id,
                command.UserId,
                command.SelectUserId
                );

            if (!_taskCatalogDbContext.UserSelections.Any(x => x.UserId == command.UserId && x.SelectUserId == command.SelectUserId))
            {
                await _taskCatalogDbContext.UserSelections.AddAsync(userSelection, cancellationToken: cancellationToken);

                await _taskCatalogDbContext.SaveChangesAsync(cancellationToken);
            }
            //await MongoRepository.AddAsync(Task);

            var created = await _taskCatalogDbContext.UserSelections
                .Include(x => x.SelectUser)
                       .SingleOrDefaultAsync(x => x.UserId == userSelection.UserId && x.SelectUserId == command.SelectUserId, cancellationToken: cancellationToken);

            var CategoryDto = _mapper.Map<UserDto>(created);

            _logger.LogInformation("UserSelection a with ID: '{CategoryId} created.'", command.Id);

            var result = new Result();

            return result.ToResult(new AddUserToSelectionResult(CategoryDto));
        }
    }
}
