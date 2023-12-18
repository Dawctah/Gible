using MongoDB.Driver;

namespace Gible.Tech.Mongo
{
    public interface IMongoClientHandler
    {
        Task WriteAsync<TDocument>(TDocument item)
    where TDocument : AggregateRoot;
        Task WriteAsync<TDocument>(IEnumerable<TDocument> items)
            where TDocument : AggregateRoot;

        Task UpdateAsync<TDocument>(TDocument item)
            where TDocument : AggregateRoot;

        IMongoCollection<TDocument> GetCollection<TDocument>()
            where TDocument : AggregateRoot;
        IEnumerable<TDocument> GetResults<TDocument>()
            where TDocument : AggregateRoot;
    }
}
