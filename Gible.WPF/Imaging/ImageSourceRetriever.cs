using System.Windows.Media.Imaging;

namespace Gible.WPF.Imaging
{
    public interface IImageSourceRetriever
    {
        public BitmapImage RetrieveImage(string filename);
    }

    public class ImageSourceRetriever : IImageSourceRetriever
    {
        public BitmapImage RetrieveImage(string filename)
        {
            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new(filename, UriKind.Absolute);
                bitmap.EndInit();

                return bitmap;
            }
            catch
            {
                // Failed to retrieve bitmap, load a backup.
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                var backup = System.Reflection.Assembly.GetEntryAssembly()!.Location.Replace("LifeCodex.WPF.dll", "Images\\failed-load.png");
                bitmap.UriSource = new(backup, UriKind.Absolute);
                bitmap.EndInit();

                return bitmap;
            }
        }
    }
}
