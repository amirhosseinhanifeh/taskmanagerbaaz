using AutoMapper;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
using MS.Services.TaskCatalog.Contract.Workflows.Dtos;
using MS.Services.TaskCatalog.Contract.Workflows.Request;

namespace MS.Services.TaskCatalog.Application.workflows.Mappers.WorkflowSteps;

public class WorkflowStepsMappers : Profile
{
    public WorkflowStepsMappers()
    {
        CreateMap<Domain.Workflows.WorkflowStep, WorkflowStepsDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.DeadLine, opt => opt.MapFrom(x => "salam"))
            .ForMember(x => x.RoleModel, opt => opt.MapFrom(x => x.WorkflowRoleModel))
            ;
        CreateMap<CreateWorkflowStepsCommand, Domain.Workflows.WorkflowStep>();
        CreateMap<CreateWorkflowStepsRequest, CreateWorkflowStepsCommand>()
            .ConstructUsing(req => new CreateWorkflowStepsCommand(
                req.Name,
                req.workflowId
                ));
    }
}


