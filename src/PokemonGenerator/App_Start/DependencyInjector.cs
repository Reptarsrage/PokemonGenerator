using Autofac;
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
        private static ILifetimeScope Scope { get; set; }

        public DependencyInjector()
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder);

            Scope = builder.Build().BeginLifetimeScope();
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
            builder.RegisterType<SettingsWindow>().InstancePerLifetimeScope();

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
            builder.RegisterType<DirectoryUtility>().As<IDirectoryUtility>();

            // Other
            builder.RegisterType<PokemonGeneratorRunner>().As<IPokemonGeneratorRunner>();
            builder.RegisterType<PokemonTeamGenerator>().As<IPokemonTeamGenerator>();
            builder.RegisterType<PokemonMoveGenerator>().As<IPokemonMoveGenerator>();
            builder.RegisterType<Random>().As<Random>().InstancePerLifetimeScope();
            builder.RegisterType<PokemonGeneratorConfig>().As<PokemonGeneratorConfig>();
            builder.RegisterType<PokeGeneratorOptions>().As<PokeGeneratorOptions>();
            builder.RegisterType<PersistentConfig>().As<PersistentConfig>();
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}