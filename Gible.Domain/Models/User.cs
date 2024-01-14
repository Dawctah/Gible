using Gible.Tech.Mongo;
using Knox.Security;

namespace Gible.Domain.Models
{
    public record User : AggregateRoot
    {
        public static User Empty { get; } = new();
        public static User Default { get; } = Empty;

        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string PasswordHash { get; init; } = string.Empty;

        public IEnumerable<string> RecentlyViewedRecipeKeys { get; init; } = [];

        public bool IsAdmin { get; init; } = false;

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
