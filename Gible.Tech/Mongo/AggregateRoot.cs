using Knox.Security;
using MongoDB.Bson.Serialization.Attributes;

namespace Gible.Tech.Mongo
{
    public abstract record AggregateRoot
    {
        [BsonId, BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Key { get; init; } = string.Empty;

        public abstract ValidationResult IsValid();
        public abstract void Validate();
    }
}
