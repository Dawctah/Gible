using Gible.WPF.Imaging;
using Microsoft.Extensions.DependencyInjection;

namespace Gible.WPF
{
    public static class DependencyInjector
    {
        public static IServiceCollection InjectWpf(this IServiceCollection services)
        {
            services
                .AddTransient<IImageSourceRetriever, ImageSourceRetriever>()
                ;

            return services;
        }
    }
}