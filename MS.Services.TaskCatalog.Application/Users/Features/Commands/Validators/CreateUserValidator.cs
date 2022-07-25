using FluentValidation;
using MS.Services.TaskCatalog.Contract.Users.Commands;
namespace MS.Services.TaskCatalog.Application.Users.Features.Commands.Validators;
public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
         
    }
}