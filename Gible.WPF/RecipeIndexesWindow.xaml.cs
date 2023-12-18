using Gible.Domain.Commands;
using Gible.Domain.Models;
using Gible.Domain.Queries;
using Knox.Mediation;
using System.Windows;

namespace Gible.WPF
{
    /// <summary>
    /// Interaction logic for RecipeIndexesWindow.xaml
    /// </summary>
    public partial class RecipeIndexesWindow : Window
    {
        private readonly IMediator mediator;
        private readonly User user;
        private readonly Func<Task> reloadRecipes;

        private Recipe recipe = Recipe.Empty;
        private Recipe Recipe
        {
            get => recipe;
            set
            {
                recipe = value;

                reloadRecipes?.Invoke();
                SetDisplays();
            }
        }

        public RecipeIndexesWindow(Recipe recipe, User user, IMediator mediator, Func<Task> reloadRecipes)
        {
            this.mediator = mediator;

            this.user = user;
            this.recipe = recipe;
            this.reloadRecipes = reloadRecipes;

            InitializeComponent();
        }

        private void RecipeIndexes_Loaded(object sender, RoutedEventArgs e)
        {
            RecipeIndexes.Title = $"{recipe.Name} Indexes";
            SetDisplays();
        }

        private void SetDisplays()
        {
            RecipeIndexesListBox.ItemsSource = Recipe.Tags;
        }

        private async void AddIndexButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AddIndexTextBox.Text))
            {
                return;
            }

            await mediator.ExecuteCommandAsync(new UpdateRecipeTagCommand(Recipe.Key, user.Key, AddIndexTextBox.Text));

            Recipe = await mediator.RequestResponseAsync<GetRecipeByKeyQuery, Recipe>(new(recipe.Key));
        }

        private void AddIndexTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            AddIndexTextBox.Text = string.Empty;
        }
    }
}
