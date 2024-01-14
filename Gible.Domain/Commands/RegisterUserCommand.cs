using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Commanding;
using Knox.Security;

namespace Gible.Domain.Commands
{
    public record RegisterUserCommand(string FirstName, string LastName, string Email, string Password) : Command;
    public class RegisterUserCommandHandler(IRepository<User> userRepository, IKnoxHasher knoxHasher) : ICommandHandler<RegisterUserCommand>
    {
        public Task<bool> CanExecuteAsync(RegisterUserCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(RegisterUserCommand command)
        {
            // Hash password.
            var passwordHash = knoxHasher.HashPassword(command.Password);

            // Verify email.
            var mailAddress = new System.Net.Mail.MailAddress(command.Email).Address;

            var user = User.Default with
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = mailAddress,
                PasswordHash = passwordHash,
            };

            await userRepository.InsertAsync(user);
        }
    }
}
