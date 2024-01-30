using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Querying;

namespace Gible.Domain.Queries
{
    public record RecipeByKeyQuery(string RecipeKey) : Query;
    public class RecipeByKeyQueryHandler(IRepository<Recipe> recipeRepository) : IQueryHandler<RecipeByKeyQuery, Recipe>
    {
        public Task<Recipe> RequestAsync(RecipeByKeyQuery query) => Task.FromResult(recipeRepository.GetResult(query.RecipeKey));
    }
}
