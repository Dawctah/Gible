using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Extensions;
using Knox.Monads;
using Knox.Querying;

namespace Gible.Domain.Queries
{
    public record GetFirstUserQuery : Query;
    public class GetFirstUserQueryHandler(IRepository<User> repository) : QueryHandler<GetFirstUserQuery, Gift<User>>
    {
        protected override Task<Gift<User>> InternalRequestAsync(GetFirstUserQuery query) => Task.FromResult(repository.GetResults().WrapFirst());
    }
}
