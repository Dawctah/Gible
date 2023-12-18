using Gible.Domain.Commands;
using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.ConsoleCommanding;
using Knox.Extensions;

namespace Gible.Console.Commands
{
    public class UpdateRecipeTagConsoleCommand(UpdateRecipeTagCommandHandler commandHandler, IRepository<Recipe> repository, IRepository<User> userRepository) : IConsoleCommand
    {
        private Recipe recipe = Recipe.Empty;

        public string CommandDocumentation => $"{CommandName} [RECIPE KEY] [TAGS (SEPARATE BY SPACE)]";

        public string CommandName => "index";

        public async Task ExecuteAsync(ConsoleCommandContext context)
        {
            recipe = repository.GetResult(context.Arguments[1]);
            var userKey = userRepository.GetResults().WrapFirst().UnwrapOrTantrum("No user has been registered.").Key;

            var tags = new List<string>();
            for (var index = 2; index < context.Arguments.Length; index++)
            {
                tags.Add(context.Arguments[index]);
            }

            var command = new UpdateRecipeTagCommand(context.Arguments[1], userKey, tags);
            await commandHandler.ExecuteAsync(command);
        }

        public string SuccessMessage(ConsoleCommandContext context) => $"Successfully updated recipe {recipe.Name}";
    }
}
