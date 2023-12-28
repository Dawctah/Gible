using Gible.Domain.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Gible.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .InjectAll()
                .InjectWpf()
                ;

            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow!.Show();
        }
    }

}
