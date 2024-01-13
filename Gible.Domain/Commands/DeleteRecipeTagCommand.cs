using Gible.Domain.Models;
using Gible.Domain.Repositories;
using Knox.Commanding;

namespace Gible.Domain.Commands
{
    public record DeleteRecipeTagCommand(string RecipeKey, string Tag) : Command;
    public class DeleteRecipeTagCommandHandler(IRepository<Recipe> repository) : ICommandHandler<DeleteRecipeTagCommand>
    {
        public Task<bool> CanExecuteAsync(DeleteRecipeTagCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync(DeleteRecipeTagCommand command)
        {
            var recipe = repository.GetResult(command.RecipeKey);

            var updatedTags = recipe.Tags.Where(tag => tag != command.Tag);
            var updatedRecipe = recipe with { Tags = updatedTags };

            await repository.UpdateAsync(updatedRecipe);
        }
    }
}