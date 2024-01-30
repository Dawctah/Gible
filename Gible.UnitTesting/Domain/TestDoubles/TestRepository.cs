using Gible.Domain.Repositories;
using Gible.Tech.Mongo;
using Knox.Extensions;

namespace Gible.UnitTesting.Domain.TestDoubles
{
    public class TestRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        public List<TAggregateRoot> Items { get; set; } = [];
        public List<TAggregateRoot> Inserted { get; set; } = [];
        public List<TAggregateRoot> Updated { get; set; } = [];

        public TAggregateRoot GetResult(string key)
        {
            return Items.Where(item => item.Key == key).WrapFirst().UnwrapOrTantrum("No items are loaded.");
        }

        public IEnumerable<TAggregateRoot> GetResults()
        {
            return Items;
        }

        public Task InsertAsync(TAggregateRoot item)
        {
            Items.Add(item);
            Inserted.Add(item);

            return Task.CompletedTask;
        }

        public Task InsertManyAsync(IEnumerable<TAggregateRoot> items)
        {
            Items.AddRange(items);
            Inserted.AddRange(items);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(TAggregateRoot item)
        {
            var initial = Items.Where(x => x.Key == item.Key).WrapFirst().UnwrapOrTantrum("Item was not found in the test repository.");
            Items.Remove(initial);
            Items.Add(item);

            Updated.Add(item);

            return Task.CompletedTask;
        }
    }
}
