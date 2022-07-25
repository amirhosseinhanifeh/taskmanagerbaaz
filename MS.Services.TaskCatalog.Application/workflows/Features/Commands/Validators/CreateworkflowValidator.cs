using FluentValidation;
using MS.Services.TaskCatalog.Contract.Workflows.Commands;
namespace MS.Services.TaskCatalog.Application.Workflows.Features.Commands.Validators;
public class CreateWorkflowValidator : AbstractValidator<CreateWorkflowCommand>
{
    public CreateWorkflowValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.name)
            .NotEmpty().WithMessage("Name is required.");
         
    }
}