using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Querying;

namespace Gible.Domain.Queries
{
    public record GetRecipeByKeyQuery(string RecipeKey) : Query;
    public class GetRecipeByKeyQueryHandler(IRepository<Recipe> recipeRepository) : IQueryHandler<GetRecipeByKeyQuery, Recipe>
    {
        public Task<Recipe> RequestAsync(GetRecipeByKeyQuery query) => Task.FromResult(recipeRepository.GetResult(query.RecipeKey));
    }
}
