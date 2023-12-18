using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Querying;

namespace Gible.Domain.Queries
{
    public record GetAllRecipesQuery : Query;
    public class GetAllRecipesQueryHandler(IRepository<Recipe> repository) : IQueryHandler<GetAllRecipesQuery, IEnumerable<Recipe>>
    {
        public Task<IEnumerable<Recipe>> RequestAsync(GetAllRecipesQuery query)
        {
            var allRecipes = repository.GetResults();
            return Task.FromResult(allRecipes);
        }
    }
}
