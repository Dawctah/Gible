using Gible.Tech.Mongo;
using Knox.Security;

namespace Gible.Domain.Models
{
    public record Recipe : AggregateRoot
    {
        public static Recipe Empty { get; } = new Recipe() with
        {
            Key = string.Empty
        };

        public static Recipe Default { get; } = new Recipe();

        public string Name { get; init; } = string.Empty;
        public IEnumerable<string> Contributors { get; init; } = Enumerable.Empty<string>();
        public IEnumerable<string> Images { get; init; } = Enumerable.Empty<string>();
        public IEnumerable<string> Tags { get; init; } = Enumerable.Empty<string>();

        public override ValidationResult IsValid()
        {
            throw new NotImplementedException();
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }

        public override string ToString() => Name;
    }
}
