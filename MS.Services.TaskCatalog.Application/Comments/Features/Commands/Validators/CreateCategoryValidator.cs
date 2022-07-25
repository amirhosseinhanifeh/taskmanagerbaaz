using FluentValidation;
using MS.Services.TaskCatalog.Contract.Categories.Commands;
using MS.Services.TaskCatalog.Contract.Comments.Commands;
using MS.Services.TaskCatalog.Contract.Tasks.Commands;
namespace MS.Services.TaskCatalog.Application.Comments.Features.Commands.Validators;
public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Id must be greater than 0");

        RuleFor(x => x.Body)
            .NotEmpty().WithMessage("body is required.");
         
    }
}