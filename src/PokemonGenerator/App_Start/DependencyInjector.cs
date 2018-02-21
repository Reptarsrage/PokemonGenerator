using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PokemonGenerator.IO;
using PokemonGenerator.Managers;
using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Providers;
using PokemonGenerator.Repositories;
using PokemonGenerator.Utilities;
using PokemonGenerator.Validators;
using PokemonGenerator.Windows;
using PokemonGenerator.Windows.Options;
using System;
using Microsoft.Extensions.Caching.Memory;

namespace PokemonGenerator
{
    public class DependencyInjector : IDisposable
    {
        protected static ILifetimeScope Scope { get; set; }
        protected static IConfigurationRoot Configuration;

        static DependencyInjector()
        {
            var builder = new ContainerBuilder();

            BuildConfiguration(builder);
            RegisterServices(builder);

            Scope = builder.Build().BeginLifetimeScope();
        }

        protected static void BuildConfiguration(ContainerBuilder builder)
        {
            var options = new PersistentConfig();
            var iOptions = Options.Create(options);

            // When in winforms designer, this directory is unset and we should not configure anything
            if (string.IsNullOrWhiteSpace(AppDomain.CurrentDomain.GetData("DataDirectory") as string))
            {
                
                builder.Register<IOptions<PersistentConfig>>(context => iOptions).InstancePerLifetimeScope();
                builder.Register<IConfiguration>(context => new ConfigurationBuilder().Build()).InstancePerLifetimeScope();
                return;
            }

            Configuration = new ConfigurationBuilder()
                .SetBasePath((string)AppDomain.CurrentDomain.GetData("DataDirectory"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(ConfigRepository.ConfigFileName, optional: true, reloadOnChange: true)
                .Build();

            Configuration.GetSection("Options").Bind(iOptions.Value.Options);
            Configuration.GetSection("Configuration").Bind(iOptions.Value.Configuration);
            builder.Register<IOptions<PersistentConfig>>(context => iOptions).InstancePerLifetimeScope();
            builder.Register<IConfiguration>(context => Configuration).InstancePerLifetimeScope();
        }

        public static object Get(Type type)
        {
            return Scope.Resolve(type);
        }

        public static T Get<T>()
        {
            return Scope.Resolve<T>();
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        protected static void RegisterServices(ContainerBuilder builder)
        {
            // Managers
            builder.RegisterType<GeneratorManager>().As<IGeneratorManager>();

            // Controls
            builder.RegisterType<MainWindow>();
            builder.RegisterType<PokemonSelectionWindow>();
            builder.RegisterType<RandomOptionsWindow>();
            builder.Register(context => new OptionsWindowController(
                context.Resolve<PokemonSelectionWindow>() ,
                context.Resolve<RandomOptionsWindow>() ,
                context.Resolve<PokemonLikelinessWindow>()));
            builder.RegisterType<PokemonLikelinessWindow>();
            builder.RegisterType<TeamSelectionWindow>();

            // IO
            builder.RegisterType<BinaryReader2>().As<IBinaryReader2>();
            builder.RegisterType<BinaryWriter2>().As<IBinaryWriter2>();
            builder.RegisterType<Charset>().As<ICharset>();

            // Validators
            builder.RegisterType<PokeGeneratorOptionsValidator>().As<IPokeGeneratorOptionsValidator>();

            // Utilities
            builder.RegisterType<PokemonStatProvider>().As<IPokemonStatProvider>();
            builder.RegisterType<ProbabilityUtility>().As<IProbabilityUtility>();

            // Providers
            builder.RegisterType<SpriteProvider>().As<ISpriteProvider>();
            builder.RegisterType<PokemonProvider>().As<IPokemonProvider>();
            builder.RegisterType<PokemonMoveProvider>().As<IPokemonMoveProvider>();
            builder.RegisterType<SaveFileProvider>().As<ISaveFileProvider>();

            // Repositories
            builder.RegisterType<PokemonRepository>().As<IPokemonRepository>();
            builder.RegisterType<SaveFileRepository>().As<ISaveFileRepository>();
            builder.RegisterType<ConfigRepository>().As<IConfigRepository>();
            builder.RegisterType<NRageConfigRepository>().As<INRageConfigRepository>();
            builder.RegisterType<P64ConfigRepository>().As<IP64ConfigRepository>();

            // Other
            builder.RegisterType<Random>().InstancePerLifetimeScope();
            builder.Register<IMemoryCache>(ctx => new MemoryCache(new MemoryCacheOptions())).InstancePerLifetimeScope();
            builder.Register<GeneratorConfig>(context => Get<IOptions<PersistentConfig>>().Value.Configuration);
            builder.Register(context => Get<IOptions<PersistentConfig>>().Value.Options);
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}