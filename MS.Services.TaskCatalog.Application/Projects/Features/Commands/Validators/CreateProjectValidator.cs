using FluentValidation;
using MS.Services.TaskCatalog.Contract.Projects.Commands;
namespace MS.Services.TaskCatalog.Application.Projects.Features.Commands.Validators;
public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
         
    }
}