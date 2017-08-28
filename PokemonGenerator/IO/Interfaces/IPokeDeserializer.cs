﻿using PokemonGenerator.Modals;

namespace PokemonGenerator.IO
{
    internal interface IPokeDeserializer
    {
        SAVFileModel ParseSAVFileModel(string filename, Charset charset);
    }
}