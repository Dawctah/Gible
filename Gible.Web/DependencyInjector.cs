using Knox.Security;

namespace Gible.Web
{
    public static class DependencyInjector
    {
        public static IServiceCollection InjectWeb(this IServiceCollection services)
        {
            services
                .AddTransient<IKnoxHasher, KnoxHasher>()
                ;

            return services;
        }
    }
}
