using Gible.Domain.Commands;
using Gible.Domain.Models;
using Gible.Domain.Queries;
using Gible.Domain.Settings;
using Gible.WPF.Imaging;
using Knox.Extensions;
using Knox.Mediation;
using Knox.Monads;
using System.Diagnostics;
using System.Windows;

namespace Gible.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMediator mediator;
        private readonly IImageSourceRetriever imageSourceRetriever;
        private readonly IApplicationSettings applicationSettings;

        private User user = User.Empty;

        private IEnumerable<Recipe> recipes = Enumerable.Empty<Recipe>();

        private Recipe SelectedRecipe => (RecipesListBox.SelectedItem as Recipe)!;

        private string SearchBoxText
        {
            get => SearchBox.Text;
            set
            {
                SearchBox.Text = value;
            }
        }

        public MainWindow(IMediator mediator, IImageSourceRetriever imageSourceRetriever, IApplicationSettings applicationSettings)
        {
            this.mediator = mediator;
            this.imageSourceRetriever = imageSourceRetriever;
            this.applicationSettings = applicationSettings;

            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var wrappedUser = await mediator.RequestResponseAsync<GetFirstUserQuery, Gift<User>>(new());

            if (wrappedUser.IsEmpty)
            {
                MessageBox.Show("No user has been registered to the database. Please run the Console version of the app and register as a user, then re-open this app.", "No User Found");
                return;
            }

            user = wrappedUser.Unwrap();
            await LoadRecipesAsync();
        }

        private void RecipesListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var recipeImageWindow = new RecipeImage(SelectedRecipe, imageSourceRetriever);
            var recipeIndexWindow = new RecipeIndexesWindow(SelectedRecipe, user, mediator, LoadRecipesAsync);

            recipeImageWindow.Show();
            recipeIndexWindow.Show();
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBoxText = string.Empty;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var tags = SearchBoxText.Split(',');
            var result = await mediator.RequestResponseAsync<GetRecipesWithTagsQuery, IEnumerable<Recipe>>(new(tags));

            RecipesListBox.ItemsSource = result;
        }

        private async void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadRecipesAsync();
        }

        private async void ExportRecipesButton_Click(object sender, RoutedEventArgs e)
        {
            var filePaths = new List<string>();
            var selectedRecipes = (RecipesListBox.ItemsSource as IEnumerable<Recipe>)!;
            foreach (var recipe in selectedRecipes)
            {
                filePaths = filePaths.Union(recipe.Images).ToList();
            }

            // Add underscore for easy finding in the folder.
            var fileName = "_" + SearchBoxText.PackageString().UnwrapOrExchange("All Recipes");

            await mediator.ExecuteCommandAsync(new ExportSelectedRecipesCommand(fileName, filePaths));

            var result = MessageBox.Show($"Exported {selectedRecipes.Count()} recipes to {applicationSettings.BaseDirectory} under the name {fileName}.\nGo to directory?", "Recipes Exported", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                var fileExplorer = Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe";
                Process.Start(fileExplorer, applicationSettings.BaseDirectory);
            }
        }

        private async Task LoadRecipesAsync()
        {
            recipes = await mediator.RequestResponseAsync<GetAllRecipesQuery, IEnumerable<Recipe>>(new GetAllRecipesQuery());

            RecipesListBox.ItemsSource = recipes;
        }
    }
}