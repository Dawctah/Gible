using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Querying;

namespace Gible.Domain.Queries
{
    public record GetRecipesWithTagsQuery(IEnumerable<string> Tags) : Query;
    public class GetRecipesWithTagsQueryHandler(IRepository<Recipe> recipeRepository) : IQueryHandler<GetRecipesWithTagsQuery, IEnumerable<Recipe>>
    {
        public Task<IEnumerable<Recipe>> RequestAsync(GetRecipesWithTagsQuery query)
        {
            var allRecipes = recipeRepository.GetResults();
            var result = new List<Recipe>();
            foreach (var tag in query.Tags)
            {
                var taggedRecipes = allRecipes.Where(recipe => recipe.Tags.Select(value => value.ToLower()).Contains(tag.ToLower()));
                result.AddRange(taggedRecipes);
            }

            return Task.FromResult(result as IEnumerable<Recipe>);
        }
    }
}