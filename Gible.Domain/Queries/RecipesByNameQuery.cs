using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Querying;

namespace Gible.Domain.Queries
{
    public record RecipesByNameQuery(IEnumerable<string> Strings) : Query;
    public class RecipesByNameQueryHandler(IRepository<Recipe> recipeRepository) : QueryHandler<RecipesByNameQuery, IEnumerable<Recipe>>
    {
        protected override Task<IEnumerable<Recipe>> InternalRequestAsync(RecipesByNameQuery query)
        {
            var recipes = recipeRepository.GetResults()
                .Where(recipe => query.Strings
                    .Any(name => recipe.Name.ToLower().Contains(name.ToLower())));

            return Task.FromResult(recipes);
        }
    }
}
