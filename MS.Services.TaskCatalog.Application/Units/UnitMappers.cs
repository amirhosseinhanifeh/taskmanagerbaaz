using AutoMapper;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Domain.Tasks;

namespace MS.Services.TaskCatalog.Application.Units;
public class UnitMappers : Profile
{
    public UnitMappers()
    {
        CreateMap<Domain.Tasks.Unit, Unit>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))

            ;


        CreateMap<CreateTaskCommand, Domain.Tasks.Task>();


    }
}