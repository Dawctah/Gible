namespace Gible.Domain.Settings
{
    public interface IApplicationSettings
    {
        string BaseDirectory { get; }
    }

    public class ApplicationSettings : IApplicationSettings
    {
        public string BaseDirectory => "C:\\Recipes";
    }
}
