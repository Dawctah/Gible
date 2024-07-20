using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Commanding;

namespace Gible.Domain.Commands
{
    public record AddRecipeTagsCommand(string RecipeKey, string UserKey, IEnumerable<string> Tags) : Command
    {
        public AddRecipeTagsCommand(string RecipeKey, string UserKey, string tag) : this(RecipeKey, UserKey, new List<string>() { tag }) { }
    }
    public class UpdateRecipeTagCommandHandler(IRepository<Recipe> recipeRepository) : CommandHandler<AddRecipeTagsCommand>
    {
        protected override Task<bool> InternalCanExecuteAsync(AddRecipeTagsCommand command)
        {
            throw new NotImplementedException();
        }

        protected override async Task InternalExecuteAsync(AddRecipeTagsCommand command)
        {
            var recipe = recipeRepository.GetResult(command.RecipeKey);

            var cleanedTags = new List<string>();
            foreach (var tag in command.Tags)
            {
                cleanedTags.Add(tag.Trim());
            }

            var tags = recipe.Tags.Union(cleanedTags);

            var updatedRecipe = recipe with
            {
                Tags = tags,
            };

            await recipeRepository.UpdateAsync(updatedRecipe);
        }
    }
}