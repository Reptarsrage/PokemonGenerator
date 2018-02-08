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

namespace PokemonGenerator
{
    public class DependencyInjector : IDisposable
    {
        private ILifetimeScope Scope { get; set; }
        private IConfigurationRoot Configuration;

        public DependencyInjector()
        {
            var builder = new ContainerBuilder();

            BuildConfiguration(builder);
            RegisterServices(builder);

            Scope = builder.Build().BeginLifetimeScope();
        }

        private void BuildConfiguration(ContainerBuilder builder)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath((string)AppDomain.CurrentDomain.GetData("DataDirectory"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile(ConfigRepository.ConfigFileName, optional: true, reloadOnChange: true)
                .Build();

            var options = new PersistentConfig();
            var iOptions = Options.Create(options);
            Configuration.GetSection("Options").Bind(iOptions.Value.Options);
            Configuration.GetSection("Configuration").Bind(iOptions.Value.Configuration);
            builder.Register<IOptions<PersistentConfig>>(context => iOptions).InstancePerLifetimeScope();
            builder.Register<IConfiguration>(context => Configuration).InstancePerLifetimeScope();
        }

        public object Get(Type type)
        {
            return Scope.Resolve(type);
        }

        public T Get<T>()
        {
            return Scope.Resolve<T>();
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private void RegisterServices(ContainerBuilder builder)
        {
            // Managers
            builder.RegisterType<GeneratorManager>().As<IGeneratorManager>();

            // Controls
            builder.RegisterType<MainWindow>().InstancePerLifetimeScope();
            builder.RegisterType<PokemonSelectionWindow>().InstancePerLifetimeScope();
            builder.RegisterType<RandomOptionsWindow>().InstancePerLifetimeScope();
            builder.RegisterType<OptionsWindowController>().InstancePerLifetimeScope();
            builder.RegisterType<PokemonLikelinessWindow>().InstancePerLifetimeScope();
            builder.RegisterType<TeamSelectionWindow>().InstancePerLifetimeScope();

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
            builder.Register<GeneratorConfig>(context => Get<IOptions<PersistentConfig>>().Value.Configuration);
            builder.Register(context => this.Get<IOptions<PersistentConfig>>().Value.Options);
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}