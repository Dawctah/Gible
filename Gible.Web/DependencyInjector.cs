namespace Gible.Web
{
    public static class DependencyInjector
    {
        public static IServiceCollection InjectWeb(this IServiceCollection services)
        {
            services
                .AddTransient<IHttpContextAccessor, HttpContextAccessor>()
                ;

            return services;
        }
    }
}
