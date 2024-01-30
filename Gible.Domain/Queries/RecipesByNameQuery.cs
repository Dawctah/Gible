using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Querying;

namespace Gible.Domain.Queries
{
    public record RecipesByNameQuery(IEnumerable<string> Strings) : Query;
    public class RecipesByNameQueryHandler(IRepository<Recipe> recipeRepository) : IQueryHandler<RecipesByNameQuery, IEnumerable<Recipe>>
    {
        public Task<IEnumerable<Recipe>> RequestAsync(RecipesByNameQuery query)
        {
            var recipes = recipeRepository.GetResults()
                .Where(recipe => query.Strings
                    .Any(name => recipe.Name.ToLower().Contains(name.ToLower())));

            return Task.FromResult(recipes);
        }
    }
}
