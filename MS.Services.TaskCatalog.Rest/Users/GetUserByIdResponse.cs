using MS.Services.TaskCatalog.Contract.Tasks.Dtos;
using MS.Services.TaskCatalog.Contract.Users.Dtos;

namespace MS.Services.TaskCatalog.Rest.Tasks;
public record GetUserByIdResponse(UserDto User);