using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Exceptions;
using Knox.Extensions;
using Knox.Querying;
using Knox.Security;

namespace Gible.Domain.Queries
{
    public record GetUserByCredentialsQuery(string Email, string Password) : Query;
    public class GetUserByCredentialsQueryHandler(IRepository<User> userRepository, IKnoxHasher knoxHasher) : IQueryHandler<GetUserByCredentialsQuery, User>
    {
        public Task<User> RequestAsync(GetUserByCredentialsQuery query)
        {
            var response = "Username or password was incorrect.";

            // We know if the email or password was incorrect but we don't want to let a user know for security.
            var user = userRepository.GetResults().Where(user => user.Email == query.Email).WrapFirst().UnwrapOrTantrum(response);

            if (!knoxHasher.CompareHash(user.PasswordHash, query.Password))
            {
                throw new ValidationException(new ValidationResult().AppendMessage(response));
            }

            return Task.FromResult(user);
        }
    }
}
