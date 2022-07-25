using FluentValidation;
using MS.Services.TaskCatalog.Contract.Categories.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
namespace MS.Services.TaskCatalog.Application.Categories.Features.Commands.Validators;
public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.name)
            .NotEmpty().WithMessage("Name is required.");
         
    }
}