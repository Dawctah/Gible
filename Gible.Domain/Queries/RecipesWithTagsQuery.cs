using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Querying;

namespace Gible.Domain.Queries
{
    public record RecipesWithTagsQuery(IEnumerable<string> Tags) : Query;
    public class RecipesWithTagsQueryHandler(IRepository<Recipe> recipeRepository) : QueryHandler<RecipesWithTagsQuery, IEnumerable<Recipe>>
    {
        protected override Task<IEnumerable<Recipe>> InternalRequestAsync(RecipesWithTagsQuery query)
        {
            var allRecipes = recipeRepository.GetResults();

            var result = allRecipes
                .Where(recipe => query.Tags
                    .Any(queriedTag => recipe.Tags
                        .Any(recipeTag => recipeTag.ToLower().Contains(queriedTag.ToLower()))
                        )
                    );

            return Task.FromResult(result);
        }
    }
}