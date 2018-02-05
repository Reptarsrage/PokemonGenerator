using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PokemonGenerator.Controls;
using PokemonGenerator.DAL;
using PokemonGenerator.Editors;
using PokemonGenerator.Forms;
using PokemonGenerator.Generators;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities;
using PokemonGenerator.Validators;
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
                .AddJsonFile(PersistentConfigManager.ConfigFileName, optional: true, reloadOnChange: true)
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
            // Controls
            builder.RegisterType<MainWindow>().InstancePerLifetimeScope();
            builder.RegisterType<PokemonSelectionWindow>().InstancePerLifetimeScope();
            builder.RegisterType<RandomOptionsWindow>().InstancePerLifetimeScope();
            builder.RegisterType<OptionsWindowController>().InstancePerLifetimeScope();
            builder.RegisterType<PokemonLikelinessWindow>().InstancePerLifetimeScope();

            // IO
            builder.RegisterType<BinaryReader2>().As<IBinaryReader2>();
            builder.RegisterType<BinaryWriter2>().As<IBinaryWriter2>();
            builder.RegisterType<PokeDeserializer>().As<IPokeDeserializer>();
            builder.RegisterType<PokeSerializer>().As<IPokeSerializer>();
            builder.RegisterType<Charset>().As<ICharset>();
            builder.RegisterType<PersistentConfigManager>().As<IPersistentConfigManager>();

            // DAL
            builder.RegisterType<PokemonDA>().As<IPokemonDA>();

            // Editors
            builder.RegisterType<NRageIniEditor>().As<INRageIniEditor>();
            builder.RegisterType<P64ConfigEditor>().As<IP64ConfigEditor>();

            // Validators
            builder.RegisterType<PokeGeneratorOptionsValidator>().As<IPokeGeneratorOptionsValidator>();

            // Utilities
            builder.RegisterType<PokemonStatUtility>().As<IPokemonStatUtility>();
            builder.RegisterType<ProbabilityUtility>().As<IProbabilityUtility>();

            // Generators
            builder.RegisterType<PokemonGeneratorRunner>().As<IPokemonGeneratorRunner>();
            builder.RegisterType<PokemonTeamGenerator>().As<IPokemonTeamGenerator>();
            builder.RegisterType<PokemonMoveGenerator>().As<IPokemonMoveGenerator>();

            // Other
            builder.RegisterType<Random>().InstancePerLifetimeScope();
            builder.Register<PokemonGeneratorConfig>(context => Get<IOptions<PersistentConfig>>().Value.Configuration);
            builder.Register<PokeGeneratorOptions>(context => Get<IOptions<PersistentConfig>>().Value.Options);
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}