using MS.Services.TaskCatalog.Contract.Users.Result;
using MsftFramework.Abstractions.CQRS.Command;
using MsftFramework.Core.IdsGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Contract.Users.Command;
public record AddUserToSelectionCommand(long UserId,long SelectUserId) : ITxCreateCommand<FluentResults.Result<AddUserToSelectionResult>>
{
    public long Id { get; init; } = SnowFlakIdGenerator.NewId();
}
