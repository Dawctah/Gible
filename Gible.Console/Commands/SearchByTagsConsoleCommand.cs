using Gible.Domain.Models;
using Gible.Domain.Queries;
using Knox.ConsoleCommanding;
using Knox.Querying;
using System.Text;

namespace Gible.Console.Commands
{
    public class SearchByTagsConsoleCommand(IQueryHandler<GetRecipesWithTagsQuery, IEnumerable<Recipe>> queryHandler) : IConsoleCommand
    {
        private IEnumerable<Recipe> recipes = Enumerable.Empty<Recipe>();

        public string CommandDocumentation => $"{CommandName} [TAGS (Separated by a space)]";

        public string CommandName => "search";

        public async Task ExecuteAsync(ConsoleCommandContext context)
        {
            var tags = new List<string>();
            for (var index = 1; index < context.Arguments.Length; index++)
            {
                tags.Add(context.Arguments[index]);
            }

            recipes = await queryHandler.RequestAsync(new(tags));
        }

        public string SuccessMessage(ConsoleCommandContext context)
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
