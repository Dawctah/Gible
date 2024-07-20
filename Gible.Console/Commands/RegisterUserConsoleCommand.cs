using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.ConsoleCommanding;
using System.Text;

namespace Gible.Console.Commands
{
    public class RegisterUserConsoleCommand(IRepository<User> userRepository) : ConsoleCommandHandler
    {
        public override string CommandDocumentation => $"{CommandName} [NAME]";

        public override string CommandName => "register";

        public async override Task ExecuteAsync(ConsoleCommand command)
        {
            var name = new StringBuilder();
            for (var index = 1; index < command.Arguments.Length; index++)
            {
                name.Append($"{command.Arguments[index]} ");
            }

            var user = User.Default with
            {
                Name = name.ToString().Trim()
            };

            await userRepository.InsertAsync(user);
        }

        public override string SuccessMessage(ConsoleCommand command) => "Registered user";
    }
}
