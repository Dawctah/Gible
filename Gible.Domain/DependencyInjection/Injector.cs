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
using Knox.Security;
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
                .AddTransient<IKnoxHasher, KnoxHasher>()
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
                .AddTransient<ICommandHandler<AddRecipeTagsCommand>, UpdateRecipeTagCommandHandler>()
                .AddTransient<ICommandHandler<ExportSelectedRecipesCommand>, ExportSelectedRecipesCommandHandler>()
                .AddTransient<ICommandHandler<IngestRecipesCommand>, InjestRecipesCommandHandler>()
                .AddTransient<ICommandHandler<DeleteRecipeTagCommand>, DeleteRecipeTagCommandHandler>()
                .AddTransient<ICommandHandler<RegisterUserCommand>, RegisterUserCommandHandler>()
                ;

            return services;
        }

        public static IServiceCollection InjectQueries(this IServiceCollection services)
        {
            services
                .AddTransient<IQueryHandler<GetAllRecipesQuery, IEnumerable<Recipe>>, GetAllRecipesQueryHandler>()
                .AddTransient<IQueryHandler<GetFirstUserQuery, Gift<User>>, GetFirstUserQueryHandler>()
                .AddTransient<IQueryHandler<GetRecipeByKeyQuery, Recipe>, GetRecipeByKeyQueryHandler>()
                .AddTransient<IQueryHandler<GetRecipesWithTagsQuery, IEnumerable<Recipe>>, GetRecipesWithTagsQueryHandler>()
                .AddTransient<IQueryHandler<GetUserByCredentialsQuery, User>, GetUserByCredentialsQueryHandler>()
                ;

            return services;
        }

        private static IServiceCollection InjectMediator(this IServiceCollection services)
        {
            services.AddTransient<IMediator>((provider) =>
            {
                var mediator = new Mediator();
                mediator
                    .Register(provider.GetRequiredService<ICommandHandler<AddRecipeTagsCommand>>())
                    .Register(provider.GetRequiredService<ICommandHandler<ExportSelectedRecipesCommand>>())
                    .Register(provider.GetRequiredService<ICommandHandler<IngestRecipesCommand>>())
                    .Register(provider.GetRequiredService<ICommandHandler<DeleteRecipeTagCommand>>())
                    .Register(provider.GetRequiredService<ICommandHandler<RegisterUserCommand>>())

                    .Register(provider.GetRequiredService<IQueryHandler<GetAllRecipesQuery, IEnumerable<Recipe>>>())
                    .Register(provider.GetRequiredService<IQueryHandler<GetFirstUserQuery, Gift<User>>>())
                    .Register(provider.GetRequiredService<IQueryHandler<GetRecipeByKeyQuery, Recipe>>())
                    .Register(provider.GetRequiredService<IQueryHandler<GetRecipesWithTagsQuery, IEnumerable<Recipe>>>())
                    .Register(provider.GetRequiredService<IQueryHandler<GetUserByCredentialsQuery, User>>())
                    ;

                return mediator;
            });

            return services;
        }
    }
}
