using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Commanding;
using Knox.Extensions;
using System.Text;
using System.Text.RegularExpressions;

namespace Gible.Domain.Commands
{
    public record IngestRecipesCommand(string InputDirectory, string OutputDirectory) : Command;
    public class InjestRecipesCommandHandler(IRepository<Recipe> recipeRepository) : ICommandHandler<IngestRecipesCommand>
    {
        private record FileInformation(string PrimaryLocation, string RecipeTitle, string NewDirectory);
        public Task<bool> CanExecuteAsync(IngestRecipesCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(IngestRecipesCommand command)
        {
            // Get the recipes based off the images in the ingest folder.
            // Copy the images into the processed folder.
            // Delete the original images.

            throw new NotImplementedException("Command is not yet complete.");

            var filePaths = Directory.GetFiles(command.InputDirectory, "*.jpg");
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