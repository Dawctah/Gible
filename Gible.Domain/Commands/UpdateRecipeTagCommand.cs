using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Commanding;

namespace Gible.Domain.Commands
{
    public record UpdateRecipeTagCommand(string RecipeKey, string UserKey, IEnumerable<string> Tags) : Command
    {
        public UpdateRecipeTagCommand(string RecipeKey, string UserKey, string tag) : this(RecipeKey, UserKey, new List<string>() { tag }) { }
    }
    public class UpdateRecipeTagCommandHandler(IRepository<Recipe> recipeRepository, IRepository<User> userRepository) : ICommandHandler<UpdateRecipeTagCommand>
    {
        public Task<bool> CanExecuteAsync(UpdateRecipeTagCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(UpdateRecipeTagCommand command)
        {
            var recipe = recipeRepository.GetResult(command.RecipeKey);
            var userName = new List<string>() { userRepository.GetResult(command.UserKey).Name };

            var tags = recipe.Tags.Union(command.Tags);
            var contributors = recipe.Contributors.Union(userName);

            var updatedRecipe = recipe with
            {
                Tags = tags,
                Contributors = contributors,
            };

            await recipeRepository.UpdateAsync(updatedRecipe);
        }
    }
}