using Ninject;
using PokemonGenerator.DAL;
using PokemonGenerator.Editors;
using PokemonGenerator.IO;
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

            // Other
            kernel.Bind<IPokemonGeneratorRunner>().To<PokemonGeneratorRunner>();
            kernel.Bind<IPokemonGeneratorWorker>().To<PokemonGeneratorWorker>();
        }

        public void Dispose()
        {
            kernel.Dispose();
        }
    }
}
