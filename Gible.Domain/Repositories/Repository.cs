using Gible.Tech.Mongo;
using Knox.Extensions;

namespace Gible.Domain.Repositories
{
    public class Repository<TItem>(IMongoClientHandler clientHandler) : IRepository<TItem>
        where TItem : AggregateRoot
    {
        private readonly IMongoClientHandler clientHandler = clientHandler;

        public async Task InsertAsync(TItem item) => await clientHandler.WriteAsync(item);

        public async Task InsertManyAsync(IEnumerable<TItem> items) => await clientHandler.WriteAsync(items);

        public async Task UpdateAsync(TItem item) => await clientHandler.UpdateAsync(item);

        public IEnumerable<TItem> GetResults() => clientHandler.GetResults<TItem>();

        public TItem GetResult(string key) => GetResults().Where(x => x.Key == key).WrapFirst().UnwrapOrTantrum();
    }
}
