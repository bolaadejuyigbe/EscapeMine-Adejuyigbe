using EscapeMine.Background.Repositories;
using EscapeMine.Core.DataSettings;
using EscapeMine.Core.Interfaces.IRepositories;
using EscapeMine.Core.Interfaces.IServices;
using EscapeMine.Core.Interfaces.IServices.Factory;
using EscapeMine.Core.Models;
using EscapeMine.Core.Services;
using EscapeMine.Core.Services.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EscapeMine.AppConsole
{
  internal  class Program
    {
        private static void Main(string[] args)
        {
            ServiceProvider services = Startup();
            IGameService gameService = services.GetService<IGameService>();

            Game game = gameService.Load();

            IEnumerable<GameResult> gameResults = gameService.Run(game);

            foreach (GameResult gameResult in gameResults)
            {
                Console.WriteLine($"Game with moves {string.Join(",", gameResult.Moves)} resulted in status: {gameResult.Status.ToString()}");
                Console.WriteLine();
            }
            Console.ReadKey();

        }

        private static ServiceProvider Startup()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile($"appsettings.json", false, true)
                  .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();

            IServiceCollection services = new ServiceCollection()
                .AddOptions()
                .Configure<DataStorageSettings>(configuration.GetSection("DataStorage"));

            services.AddScoped<IMoveFactory, MoveFactory>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IPlayerService, PlayerService>();

            services.AddScoped<IBoardCoordinatesRepository, BoardCoordinatesRepository>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<ITextFileRepository, TextFileRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();

            services.RegisterAllTypes<IMoveService>(new[] { typeof(MoveFactory).Assembly });

            return services.BuildServiceProvider();

        }
    }

    public static class ServiceCollectionExtension
    {
        public static void RegisterAllTypes<T>(this IServiceCollection services, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            IEnumerable<TypeInfo> typesFromAssemblies = assemblies.SelectMany(x => x.DefinedTypes.Where(c => c.GetInterfaces().Contains(typeof(T))));
            foreach (TypeInfo type in typesFromAssemblies)
            {
                services.Add(new ServiceDescriptor(typeof(T), type, lifetime));
            }
        }
    }
}
