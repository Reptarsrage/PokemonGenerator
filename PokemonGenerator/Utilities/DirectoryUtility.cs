using PokemonGenerator.Utilities.Interfaces;
using System;
using System.IO;
using System.Reflection;

namespace PokemonGenerator.Utilities
{
    public class DirectoryUtility : IDirectoryUtility
    {
        private readonly string _assemblyDirectory;
        private readonly string _outputDirectory;
        private readonly string _contentDirectory;

        public string AssemblyDirectory() => _assemblyDirectory;

        public string ContentDirectory() => _contentDirectory;

        public string OutputDirectory() => _outputDirectory;

        public DirectoryUtility()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            _assemblyDirectory = Path.GetDirectoryName(path);

#if (DEBUG)
            AppDomain.CurrentDomain.SetData("DataDirectory", _assemblyDirectory);
            _contentDirectory = _assemblyDirectory;
            _outputDirectory = Path.Combine(_contentDirectory, "Output");
#else
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\"));
            _contentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"PokemonGenerator\");
            _outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
#endif
        }
    }
}