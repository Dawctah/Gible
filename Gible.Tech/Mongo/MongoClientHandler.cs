using MongoDB.Driver;

namespace Gible.Tech.Mongo
{
    public class MongoClientHandler : IMongoClientHandler
    {
        private readonly IMongoDatabase database;

        public MongoClientHandler()
        {
            var configuration = new MongoConfiguration();
            var mongoClient = new MongoClient(configuration.MongoConnectionString);
            database = mongoClient.GetDatabase(configuration.DatabaseName);
        }

        public async Task WriteAsync<TDocument>(TDocument item)
            where TDocument : AggregateRoot => await GetCollection<TDocument>().InsertOneAsync(item);

        public async Task WriteAsync<TDocument>(IEnumerable<TDocument> items)
            where TDocument : AggregateRoot => await GetCollection<TDocument>().InsertManyAsync(items);

        public async Task UpdateAsync<TDocument>(TDocument item)
            where TDocument : AggregateRoot
        {
            var filter = Builders<TDocument>.Filter.Eq("Key", item.Key);
            await GetCollection<TDocument>().ReplaceOneAsync(filter, item);
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>()
            where TDocument : AggregateRoot => database.GetCollection<TDocument>(typeof(TDocument).Name);

        public IEnumerable<TDocument> GetResults<TDocument>()
            where TDocument : AggregateRoot => GetCollection<TDocument>().AsQueryable().AsEnumerable();
    }
}
