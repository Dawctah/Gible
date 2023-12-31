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
            var filePaths = Directory.GetFiles(command.InputDirectory, "*.jpg");
            var recipes = new List<Recipe>();
            var nameSections = new Dictionary<string, (List<string> input, List<string> output)>();
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
                    nameSections.Add(nameSanitized, ([], []));
                }
                catch
                {
                    // If this fails, it's because the dictionary already has the key. Move on.
                }
                finally
                {
                    nameSections[nameSanitized].input.Add(file);
                    nameSections[nameSanitized].output.Add(file.Replace(command.InputDirectory, command.OutputDirectory));
                }
            }

            // Create recipe.
            foreach (var (recipeName, (input, output)) in nameSections)
            {
                // Remove everything up until wwwroot so the server knows what folder to pull from.
                var finalOutput = output.Select(value => value[(value.IndexOf("Processed"))..]);

                var recipe = Recipe.Default with
                {
                    Name = recipeName,
                    Images = finalOutput,
                };

                recipes.Add(recipe);
            }

            // Move input images into output folder.
            foreach (var (x, (input, output)) in nameSections)
            {
                for (var index = 0; index < input.Count; index++)
                {
                    File.Copy(input[index], output[index], true);
                    File.Delete(input[index]);
                }
            }

            // Now we enter the recipes into the database.
            await recipeRepository.InsertManyAsync(recipes);
        }
    }
}