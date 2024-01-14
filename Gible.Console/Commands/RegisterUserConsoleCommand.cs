using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.ConsoleCommanding;
using System.Text;

namespace Gible.Console.Commands
{
    public class RegisterUserConsoleCommand(IRepository<User> userRepository) : IConsoleCommand
    {
        public string CommandDocumentation => $"{CommandName} [NAME]";

        public string CommandName => "register";

        public async Task ExecuteAsync(ConsoleCommandContext context)
        {
            var name = new StringBuilder();
            for (var index = 1; index < context.Arguments.Length; index++)
            {
                name.Append($"{context.Arguments[index]} ");
            }

            var user = User.Default with
            {
                FirstName = name.ToString().Trim()
            };

            await userRepository.InsertAsync(user);
        }

        public string SuccessMessage(ConsoleCommandContext context) => "Registered user";
    }
}
