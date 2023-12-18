using Gible.Tech.Mongo;

namespace Gible.Domain.Repositories
{
    public interface IRepository<TItem>
        where TItem : AggregateRoot
    {
        Task InsertAsync(TItem item);
        Task InsertManyAsync(IEnumerable<TItem> items);
        Task UpdateAsync(TItem item);
        IEnumerable<TItem> GetResults();
        TItem GetResult(string key);
    }
}