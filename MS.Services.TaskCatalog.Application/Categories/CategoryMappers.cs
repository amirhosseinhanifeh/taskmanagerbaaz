using AutoMapper;
using MS.Services.TaskCatalog.Contract.Categories.Commands;
using MS.Services.TaskCatalog.Contract.Categories.Dtos;
using MS.Services.TaskCatalog.Contract.Categories.Request;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Tasks.Request;
using MS.Services.TaskCatalog.Domain.Tasks;

namespace MS.Services.TaskCatalog.Application.Categories;
public class CategoryMappers : Profile
{
    public CategoryMappers()
    {
        CreateMap<Domain.Tasks.Category, CategoryDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name))
            ;


        CreateMap<CreateCategoryCommand, Domain.Tasks.Category>();

        CreateMap<CreateCategoryRequest, CreateCategoryCommand>()
            .ConstructUsing(req => new CreateCategoryCommand(
                req.Name, req.Description));
    }
}