using AutoMapper;
using MS.Services.TaskCatalog.Contract.workflows.Dtos;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Contract.Workflows.Request;
using MS.Services.TaskCatalog.Domain.workflows;
using MS.Services.TaskCatalog.Domain.Workflows;
using MS.Services.TaskCatalog.Domain.Workflows.Models;
using MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.EnumBuilderExtensions;
using System.Linq;
namespace MS.Services.TaskCatalog.Application.workflows.Mappers.Workflow;
public class WorkflowMappers : Profile
{
    public WorkflowMappers()
    {
        CreateMap<Domain.Workflows.Workflow, WorkflowsDto>()
    .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name.Value))
    .ForMember(x => x.StepsCount, opt => opt.MapFrom(x => x.WorkflowSteps.Count))
    .ForMember(x => x.Steps, opt => opt.MapFrom(m => m.WorkflowSteps.Select(h => new WorkflowStepsDto
    {
        Id = h.Id,
        Name = h.Name,
    })))
    ;
        CreateMap<Domain.Workflows.Workflow, WorkflowDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name.Value))
            .ForMember(x => x.StepsCount, opt => opt.MapFrom(x => x.WorkflowSteps.Count))
            .ForMember(x => x.Steps, opt => opt.MapFrom(m => m.WorkflowSteps.Select(h => new WorkflowStepsDto
            {
                Id = h.Id,
                Name = h.Name,
                DeadLine = h.DeadLine.ToHHMM(),
                RoleModel = new RoleModelDto
                {
                    Id = h.WorkflowRoleModelId,
                    Name = h.WorkflowRoleModel.Name,
                    Roles = h.WorkflowRoleModel.Roles.Select(g => new RolesDto
                    {
                        Name = g.Name,
                        RoleId = g.Id
                    }).ToList()
                }
            })))
            ;

        CreateMap<Domain.Workflows.WorkflowInstance, WorkflowInstanceDto>()
        .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.StepsCount, opt => opt.MapFrom(x => x.workflowSteps.Count()))
            .ForMember(x => x.StatusTxt, opt => opt.MapFrom(x => x.Status.ToDisplay()))
            .ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
            .ForMember(x => x.Steps, opt => opt.MapFrom(x => x.workflowSteps.OrderBy(x=>x.Order).ToList()))

            ;
        CreateMap<Domain.Workflows.WorkflowStepInstance, WorkflowInstaceStepsDto>()
.ForMember(x => x.StatusTxt, opt => opt.MapFrom(x => x.Status.ToDisplay()))
.ForMember(x => x.Status, opt => opt.MapFrom(x => x.Status))
.ForMember(x => x.IsProgress, opt => opt.MapFrom(x => x.Status == WorkflowStatus.InProgress))
.ForMember(x => x.IsDone, opt => opt.MapFrom(x => x.Status == WorkflowStatus.Done))
.ForMember(x => x.IsFailed, opt => opt.MapFrom(x => x.Status == WorkflowStatus.Fail))
.ForMember(x => x.IsPending, opt => opt.MapFrom(x => x.Status == WorkflowStatus.Pending))
    ;
        CreateMap<WorkFlowAlerts, WorkflowAlertDto>();


        CreateMap<CreateWorkflowCommand, Domain.Workflows.Workflow>();
        CreateMap<CreateWorkflowRequest, CreateWorkflowCommand>()
            .ConstructUsing(req => new CreateWorkflowCommand(
                req.Name, map(req.WorkflowSteps)
                ));

        CreateMap<InitWorkflowRequest, InitWorkflowCommand>()
          .ConstructUsing(req => new InitWorkflowCommand(
              req.Name, req.WorkflowId, req.workflowInstanceId, req.Description, req.workflowStepId, req.Roles
              ));

        CreateMap<StartWorkflowRequest, StartWorkflowCommand>()
          .ConstructUsing(req => new StartWorkflowCommand(
              req.WorkflowInstanceId
              ));


        CreateMap<CreateAlertRequest, CreateAlertCommand>()
          .ConstructUsing(req => new CreateAlertCommand(
              req.Body
              ));
        CreateMap<WorkflowRoleModel, RoleModelDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
            ;
        CreateMap<Role, RolesDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.RoleId, opt => opt.MapFrom(x => x.RoleId))
            ;
        CreateMap<CreateworkflowRoleModelRequest, CreatetWorkflowRoleModelCommand>()
  .ConstructUsing(req => new CreatetWorkflowRoleModelCommand(
      req.Name,
      req.UnitId,
      req.Roles
      ));
        CreateMap<AssingAlertToRoleRequest[], AssignAlertToRoleCommand>()
  .ConstructUsing(req => new AssignAlertToRoleCommand(mapalert(req)));
    }

    private WorkflowStepAlertDto[] mapalert(IList<AssingAlertToRoleRequest> alerts)
    {
        IList<WorkflowStepAlertDto> result = new List<WorkflowStepAlertDto>();
        foreach (var item in alerts)
        {

            result.Add(new WorkflowStepAlertDto()
            {
                WorkflowAlertId = item.AlertId,
                Delay = item.Delay,
                Order = item.Order
                //WorkflowStepAlerts = item.
            });
        }
        return result.ToArray();
    }

    private WorkflowStepDto[] map(IList<WorkflowStepRequest> workflowSteps)
    {
        IList<WorkflowStepDto> result = new List<WorkflowStepDto>();
        foreach (var item in workflowSteps)
        {

            result.Add(new WorkflowStepDto()
            {
                Name = item.Name,
                Deadline = item.Deadline,
                Order = item.Order,
                WorkflowRoleModelId = item.WorkflowRoleModelId
                //WorkflowStepAlerts = item.
            });
        }
        return result.ToArray();
    }
}
