using Gible.Domain.Commands;
using Gible.Domain.Settings;
using Knox.ConsoleCommanding;

namespace Gible.Console.Commands
{
    public class InitializeRecipesConsoleCommand(InitializeRecipesCommandHandler initializeRecipesCommand, IApplicationSettings applicationSettings) : IConsoleCommand
    {
        public string CommandDocumentation => CommandName;

        public string CommandName => "initialize";

        public async Task ExecuteAsync(ConsoleCommandContext context)
        {
            await initializeRecipesCommand.ExecuteAsync(new InitializeRecipesCommand(applicationSettings.BaseDirectory));
        }

        public string SuccessMessage(ConsoleCommandContext context) => "Successfully initialized recipes.";
    }
}
