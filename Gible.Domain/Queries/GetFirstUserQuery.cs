using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Extensions;
using Knox.Monads;
using Knox.Querying;

namespace Gible.Domain.Queries
{
    public record GetFirstUserQuery : Query;
    public class GetFirstUserQueryHandler(IRepository<User> repository) : IQueryHandler<GetFirstUserQuery, Gift<User>>
    {
        public Task<Gift<User>> RequestAsync(GetFirstUserQuery query) => Task.FromResult(repository.GetResults().WrapFirst());
    }
}
