using Gible.Domain.Commands;
using Gible.Domain.Settings;
using Knox.ConsoleCommanding;

namespace Gible.Console.Commands
{
    public class InitializeRecipesConsoleCommand(InitializeRecipesCommandHandler initializeRecipesCommand, IApplicationSettings applicationSettings) : ConsoleCommandHandler
    {
        public override string CommandDocumentation => CommandName;

        public override string CommandName => "initialize";

        public async override Task ExecuteAsync(ConsoleCommand command)
        {
            await initializeRecipesCommand.ExecuteAsync(new InitializeRecipesCommand(applicationSettings.BaseDirectory));
        }

        public override string SuccessMessage(ConsoleCommand command) => "Successfully initialized recipes.";
    }
}
