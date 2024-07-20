using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Querying;

namespace Gible.Domain.Queries
{
    public record GetAllRecipesQuery : Query;
    public class GetAllRecipesQueryHandler(IRepository<Recipe> repository) : QueryHandler<GetAllRecipesQuery, IEnumerable<Recipe>>
    {
        protected override Task<IEnumerable<Recipe>> InternalRequestAsync(GetAllRecipesQuery query)
        {
            var allRecipes = repository.GetResults();
            return Task.FromResult(allRecipes);
        }
    }
}
