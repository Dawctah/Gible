using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Commanding;
using Knox.Extensions;
using System.Text;
using System.Text.RegularExpressions;

namespace Gible.Domain.Commands
{
    public record InitializeRecipesCommand(string Path) : Command;
    public class InitializeRecipesCommandHandler(IRepository<Recipe> recipeRepository) : ICommandHandler<InitializeRecipesCommand>
    {
        public Task<bool> CanExecuteAsync(InitializeRecipesCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(InitializeRecipesCommand command)
        {
            // Find all images in the folder path.
            var filePaths = Directory.GetFiles(command.Path, "*.jpg");
            var recipes = new List<Recipe>();
            var nameSections = new Dictionary<string, List<string>>();
            foreach (var file in filePaths)
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                var nameCleaned = file.Split("\\")
                    .Last()
                    .Replace("-", " ")
                    .Replace(".jpg", string.Empty)
                    ;

                var nameRegexed = Regex.Replace(nameCleaned, "[0-9]", string.Empty).TrimEnd();
                var nameSanitized = nameRegexed.ToTitleCase();

                try
                {
                    nameSections.Add(nameSanitized, []);
                }
                catch
                {
                    // If this fails, it's because the dictionary already has the key. Move on.
                }
                finally
                {
                    nameSections[nameSanitized].Add(file);
                }
            }

            // Create recipe.
            foreach (var (recipeName, images) in nameSections)
            {
                var recipe = Recipe.Default with
                {
                    Name = recipeName,
                    Images = images,
                };

                recipes.Add(recipe);
            }

            // Now we enter the recipes into the database.
            await recipeRepository.InsertManyAsync(recipes);
        }
    }
}
