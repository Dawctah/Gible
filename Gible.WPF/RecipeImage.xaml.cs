using Gible.Domain.Models;
using Gible.WPF.Imaging;
using System.Windows;

namespace Gible.WPF
{
    /// <summary>
    /// Interaction logic for RecipeImage.xaml
    /// </summary>
    public partial class RecipeImage : Window
    {
        private readonly Recipe recipe;
        private readonly IImageSourceRetriever imageSourceRetriever;

        private int page = 1;

        public RecipeImage(Recipe recipe, IImageSourceRetriever imageSourceRetriever)
        {
            this.recipe = recipe;
            this.imageSourceRetriever = imageSourceRetriever;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RecipeImageWindow.Title = recipe.Name;
            UpdateDisplay();
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (page > 1)
            {
                page--;
            }

            UpdateDisplay();
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (page < recipe.Images.Count())
            {
                page++;
            }

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            // Subtract 1 for array notation.
            RecipeImageDisplay.Source = imageSourceRetriever.RetrieveImage(recipe.Images.ToList()[page - 1]);
            PageLabel.Content = $"{page}/{recipe.Images.Count()}";
        }
    }
}
