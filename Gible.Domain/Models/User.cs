using Gible.Tech.Mongo;
using Knox.Security;

namespace Gible.Domain.Models
{
    public record User : AggregateRoot
    {
        public static User Empty { get; } = new();
        public static User Default { get; } = Empty with
        {
            Role = UserRole.CheesePizza
        };

        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string PasswordHash { get; init; } = string.Empty;

        public IEnumerable<string> RecentlyViewedRecipeKeys { get; init; } = [];

        public UserRole Role { get; init; } = UserRole.Unknown;

        public override ValidationResult IsValid()
        {
            throw new NotImplementedException();
        }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }

    public enum UserRole
    {
        Unknown,

        /// <summary>
        /// User is an administrator and has full access to Gible.
        /// </summary>
        Admin,

        /// <summary>
        /// User has nothing special. They are cheese pizza. Still good though.
        /// </summary>
        CheesePizza
    }
}
