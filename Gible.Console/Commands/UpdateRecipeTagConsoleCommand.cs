using Gible.Domain.Commands;
using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.ConsoleCommanding;
using Knox.Extensions;

namespace Gible.Console.Commands
{
    public class UpdateRecipeTagConsoleCommand(UpdateRecipeTagCommandHandler commandHandler, IRepository<Recipe> repository, IRepository<User> userRepository) : ConsoleCommandHandler
    {
        private Recipe recipe = Recipe.Empty;

        public override string CommandDocumentation => $"{CommandName} [RECIPE KEY] [TAGS (SEPARATE BY SPACE)]";

        public override string CommandName => "index";


        public async override Task ExecuteAsync(ConsoleCommand command)
        {
            recipe = repository.GetResult(command.Arguments[1]);
            var userKey = userRepository.GetResults().WrapFirst().UnwrapOrTantrum("No user has been registered.").Key;

            var tags = new List<string>();
            for (var index = 2; index < command.Arguments.Length; index++)
            {
                tags.Add(command.Arguments[index]);
            }

            var addRecipeCommand = new AddRecipeTagsCommand(command.Arguments[1], userKey, tags);
            await commandHandler.ExecuteAsync(addRecipeCommand);
        }

        public override string SuccessMessage(ConsoleCommand command) => $"Successfully updated recipe {recipe.Name}";
    }
}
