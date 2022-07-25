using AutoMapper;
using MS.Services.TaskCatalog.Domain.Projects;
using MS.Services.TaskCatalog.Contract.Projects.Commands;
using MS.Services.TaskCatalog.Contract.Projects.Dtos;
using MS.Services.TaskCatalog.Contract.Projects.Request;
using MS.Services.TaskCatalog.Domain.Projects;

namespace MS.Services.TaskCatalog.Application.Projects;
public class ProjectMappers : Profile
{
    public ProjectMappers()
    {
        CreateMap<Project, ProjectDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name.Value))
            .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description));
            

        CreateMap<CreateProjectCommand, Project>();

        CreateMap<CreateProjectRequest, CreateProjectCommand>()
            .ConstructUsing(req => new CreateProjectCommand(
                req.Name,req.Description ));
    }
}