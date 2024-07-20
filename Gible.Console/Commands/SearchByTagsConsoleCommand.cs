using Gible.Domain.Models;
using Gible.Domain.Queries;
using Knox.ConsoleCommanding;
using Knox.Querying;
using System.Text;

namespace Gible.Console.Commands
{
    public class SearchByTagsConsoleCommand(QueryHandler<RecipesWithTagsQuery, IEnumerable<Recipe>> queryHandler) : ConsoleCommandHandler
    {
        private IEnumerable<Recipe> recipes = Enumerable.Empty<Recipe>();

        public override string CommandDocumentation => $"{CommandName} [TAGS (Separated by a space)]";

        public override string CommandName => "search";

        public async override Task ExecuteAsync(ConsoleCommand command)
        {
            var tags = new List<string>();
            for (var index = 1; index < command.Arguments.Length; index++)
            {
                tags.Add(command.Arguments[index]);
            }

            recipes = await queryHandler.RequestAsync(new(tags));
        }

        public override string SuccessMessage(ConsoleCommand command)
        {
            var result = new StringBuilder($"Found {recipes.Count()} recipes:\n");

            foreach (var recipe in recipes)
            {
                result.Append($"{recipe.Name}\n\t");
                foreach (var tag in recipe.Tags)
                {
                    result.Append(tag);

                    if (tag != recipe.Tags.Last())
                    {
                        result.Append(", ");
                    }
                }

                if (recipe != recipes.Last())
                {
                    result.AppendLine();
                }
            }

            return result.ToString();
        }
    }
}
