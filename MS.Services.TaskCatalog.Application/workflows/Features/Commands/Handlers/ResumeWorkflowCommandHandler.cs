using Ardalis.GuardClauses;
using AutoMapper;
using FluentResults;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MS.Services.TaskCatalog.Domain.Workflows;
using MS.Services.TaskCatalog.Infrastructure;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Abstractions.CQRS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Application.workflows.Features.Commands.Handlers
{
    public class ResumeWorkflowCommandHandler : ICommandHandler<ResumeWorkflowCommand, Unit>
    {
        #region Ctor

        private readonly ILogger<StartWorkflowCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly ITaskCatalogDbContext _taskCatalogDbContext;
        private readonly IMediator mediator;
        private readonly ICommandProcessor commandProcessor;
        private readonly IQueryProcessor queryProcessor;
        public ResumeWorkflowCommandHandler(
                 ITaskCatalogDbContext taskCatalogDbContext,
                 IMapper mapper,
                 IMediator mediator,
                 ILogger<StartWorkflowCommandHandler> logger,
                 ICommandProcessor commandProcessor,
                 IQueryProcessor queryProcessor
            )
        {
            _logger = Guard.Against.Null(logger, nameof(logger));
            _mapper = Guard.Against.Null(mapper, nameof(mapper));
            _taskCatalogDbContext = Guard.Against.Null(taskCatalogDbContext, nameof(taskCatalogDbContext));
            this.mediator = mediator;
            this.commandProcessor = commandProcessor;
            this.queryProcessor = queryProcessor;
        }
        #endregion

        public async Task<Result<Unit>> Handle(ResumeWorkflowCommand query, CancellationToken cancellationToken)
        {

         await  HandleRequest(query.workflowInstanceId);


            return await Task.FromResult(Result.Ok(Unit.Value));
        }

        public async Task HandleRequest(long workflowInstanceId)
        {
            var workflow = _taskCatalogDbContext.WorkflowInstance.Include(x => x.workflowSteps)
                .ThenInclude(x => x.WorkflowRoleUsers).FirstOrDefault(e => e.Id == workflowInstanceId);
            if (workflow == null) return;

            workflow.ChangeStatus(WorkflowStatus.InProgress);

            _taskCatalogDbContext!.SaveChanges();

            await Task.CompletedTask;
        }
    }
}
