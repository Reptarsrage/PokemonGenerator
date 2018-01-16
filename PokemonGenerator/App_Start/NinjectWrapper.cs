using Ninject;
using PokemonGenerator.DAL;
using PokemonGenerator.Editors;
using PokemonGenerator.Generators;
using PokemonGenerator.Interfaces;
using PokemonGenerator.IO;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities;
using PokemonGenerator.Validators;
using System;

namespace PokemonGenerator
{
    public class NinjectWrapper : IDisposable
    {
        private readonly IKernel kernel;

        public NinjectWrapper()
        {
            kernel = new StandardKernel();
            RegisterServices();
        }

        public T Get<T>()
        {
            return kernel.Get<T>();
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private void RegisterServices()
        {
            // IO
            kernel.Bind<IBinaryReader2>().To<BinaryReader2>();
            kernel.Bind<IBinaryWriter2>().To<BinaryWriter2>();
            kernel.Bind<IPokeDeserializer>().To<PokeDeserializer>();
            kernel.Bind<IPokeSerializer>().To<PokeSerializer>();
            kernel.Bind<ICharset>().To<Charset>();
            kernel.Bind<IPersistentConfigManager>().To<PersistentConfigManager>();

            // DAL
            kernel.Bind<IPokemonDA>().To<PokemonDA>();

            // Editors
            kernel.Bind<INRageIniEditor>().To<NRageIniEditor>();
            kernel.Bind<IP64ConfigEditor>().To<P64ConfigEditor>();

            // Validators
            kernel.Bind<IPokeGeneratorOptionsValidator>().To<PokeGeneratorOptionsValidator>();

            // Utilities
            kernel.Bind<IPokemonStatUtility>().To<PokemonStatUtility>();
            kernel.Bind<IProbabilityUtility>().To<ProbabilityUtility>();

            // Other
            kernel.Bind<IPokemonGeneratorRunner>().To<PokemonGeneratorRunner>();
            kernel.Bind<IPokemonTeamGenerator>().To<PokemonTeamGenerator>();
            kernel.Bind<IPokemonMoveGenerator>().To<PokemonMoveGenerator>();
            kernel.Bind<Random>().To<Random>().InSingletonScope();
            kernel.Bind<PokemonGeneratorConfig>().To<PokemonGeneratorConfig>();
            kernel.Bind<PokeGeneratorOptions>().To<PokeGeneratorOptions>();
            kernel.Bind<PersistentConfig>().To<PersistentConfig>();
        }

        public void Dispose()
        {
            kernel.Dispose();
        }
    }
}
