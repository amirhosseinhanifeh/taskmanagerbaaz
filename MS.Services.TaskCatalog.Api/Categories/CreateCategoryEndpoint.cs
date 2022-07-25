using Ardalis.GuardClauses;
using AutoMapper;
using MS.Services.TaskCatalog.Contract.Categories.Commands;
using MS.Services.TaskCatalog.Contract.Categories.Request;
using MS.Services.TaskCatalog.Contract.Categories.Result;
using MsftFramework.Abstractions.CQRS.Command;

namespace MS.Services.TaskCatalog.Api.Categories
{
    public static class CreateCategoryEndpoint
    {
        internal static IEndpointRouteBuilder MapCreateCategoriesEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost($"{CategoryConfigs.CategoryPrefixUri}/create", CreateCategories)
                .WithTags(CategoryConfigs.Tag)
                //.RequireAuthorization()
                .Produces<CreateCategoryResult>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("CreateCategory")
                .WithDisplayName("Create a new Category.");

            return endpoints;
        }

        private static async Task<IResult> CreateCategories(
           CreateCategoryRequest request,
           ICommandProcessor commandProcessor,
           IMapper mapper,
           CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));

            var command = mapper.Map<CreateCategoryCommand>(request);
            var result = await commandProcessor.SendAsync(command, cancellationToken);

            return Results.CreatedAtRoute("GetCategoryById", new { id = result.Value.Category.Id }, result);
        }
    }
}
