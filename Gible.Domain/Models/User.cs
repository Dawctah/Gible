using Gible.Tech.Mongo;
using Knox.Domain.Security;

namespace Gible.Domain.Models
{
    public record User : AggregateRoot
    {
        public static User Empty { get; } = new();
        public static User Default { get; } = Empty;

        public string Name { get; init; } = string.Empty;

        public override ValidationResult IsValid()
        {
            throw new NotImplementedException();
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
