using Gible.Domain.Commands;
using Gible.Domain.Models;
using Gible.Domain.Queries;
using Gible.Domain.Repositories;
using Gible.Domain.Settings;
using Gible.Tech.Mongo;
using Gible.Tech.PDF;
using Knox.Commanding;
using Knox.Mediation;
using Knox.Monads;
using Knox.Querying;
using Microsoft.Extensions.DependencyInjection;

namespace Gible.Domain.DependencyInjection
{
    public static class Injector
    {
        public static IServiceCollection InjectAll(this IServiceCollection services)
        {
            services
                .AddTransient<IApplicationSettings, ApplicationSettings>()
                .AddTransient<IMongoClientHandler, MongoClientHandler>()
                .AddTransient<IPdfExporter, PdfExporter>()
                .InjectRepositories()
                .InjectCommands()
                .InjectQueries()
                .InjectMediator()
                ;

            return services;
        }

        public static IServiceCollection InjectRepositories(this IServiceCollection services)
        {
            services
                .AddTransient<IRepository<Recipe>, Repository<Recipe>>()
                .AddTransient<IRepository<User>, Repository<User>>()
                ;

            return services;
        }

        public static IServiceCollection InjectCommands(this IServiceCollection services)
        {
            services
                .AddTransient<CommandHandler<AddRecipeTagsCommand>, UpdateRecipeTagCommandHandler>()
                .AddTransient<CommandHandler<ExportSelectedRecipesCommand>, ExportSelectedRecipesCommandHandler>()
                .AddTransient<CommandHandler<IngestRecipesCommand>, InjestRecipesCommandHandler>()
                .AddTransient<CommandHandler<DeleteRecipeTagCommand>, DeleteRecipeTagCommandHandler>()
                ;

            return services;
        }

        public static IServiceCollection InjectQueries(this IServiceCollection services)
        {
            services
                .AddTransient<QueryHandler<GetAllRecipesQuery, IEnumerable<Recipe>>, GetAllRecipesQueryHandler>()
                .AddTransient<QueryHandler<GetFirstUserQuery, Gift<User>>, GetFirstUserQueryHandler>()
                .AddTransient<QueryHandler<RecipeByKeyQuery, Recipe>, RecipeByKeyQueryHandler>()
                .AddTransient<QueryHandler<RecipesWithTagsQuery, IEnumerable<Recipe>>, RecipesWithTagsQueryHandler>()
                .AddTransient<QueryHandler<RecipesByNameQuery, IEnumerable<Recipe>>, RecipesByNameQueryHandler>()
                ;

            return services;
        }

        private static IServiceCollection InjectMediator(this IServiceCollection services)
        {
            services.AddTransient<IMediator>((provider) =>
            {
                var mediator = new Mediator();
                mediator
                    .Register(provider.GetRequiredService<CommandHandler<AddRecipeTagsCommand>>())
                    .Register(provider.GetRequiredService<CommandHandler<ExportSelectedRecipesCommand>>())
                    .Register(provider.GetRequiredService<CommandHandler<IngestRecipesCommand>>())
                    .Register(provider.GetRequiredService<CommandHandler<DeleteRecipeTagCommand>>())

                    .Register(provider.GetRequiredService<QueryHandler<GetAllRecipesQuery, IEnumerable<Recipe>>>())
                    .Register(provider.GetRequiredService<QueryHandler<GetFirstUserQuery, Gift<User>>>())
                    .Register(provider.GetRequiredService<QueryHandler<RecipeByKeyQuery, Recipe>>())
                    .Register(provider.GetRequiredService<QueryHandler<RecipesWithTagsQuery, IEnumerable<Recipe>>>())
                    .Register(provider.GetRequiredService<QueryHandler<RecipesByNameQuery, IEnumerable<Recipe>>>())
                    ;

                return mediator;
            });

            return services;
        }
    }
}
