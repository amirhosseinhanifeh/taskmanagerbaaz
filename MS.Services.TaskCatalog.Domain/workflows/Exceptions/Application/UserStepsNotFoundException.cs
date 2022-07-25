using MsftFramework.Core.Exception.Types;
namespace MS.Services.TaskCatalog.Domain.Workflows.Exceptions.Application;

public class UserStepsNotFoundException : NotFoundException
{
    public UserStepsNotFoundException(string message) : base(message)
    {
    }
    public UserStepsNotFoundException() : base($"UserSteps with not found") { }
}


public class UserStepsByIdNotFoundException : NotFoundException
{
    public UserStepsByIdNotFoundException(string message) : base(message)
    {
    }
    public UserStepsByIdNotFoundException(long id) : base($"UserSteps with with id '{id}' not found") { }
}
