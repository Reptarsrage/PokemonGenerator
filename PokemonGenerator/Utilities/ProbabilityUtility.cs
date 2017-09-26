using PokemonGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGenerator.Utilities
{
    class ProbabilityUtility : IProbabilityUtility
    {
        private readonly Random _random;
        private readonly PokemonGeneratorConfig _pokemonGeneratorConfig;

        public ProbabilityUtility(Random random, PokemonGeneratorConfig pokemonGeneratorConfig)
        {
            _random = random;
            _pokemonGeneratorConfig = pokemonGeneratorConfig;
        }

        /// <summary>
        /// Use Box-Muller transform to simulate a gaussian distribution 
        /// with a mean of <see cref="PokemonGeneratorConfig.StandardDeviation"/> 
        /// and a standard deviation of <see cref="PokemonGeneratorConfig.StandardDeviation"/>.
        /// </summary>
        /// <param name="low">The low bound</param>
        /// <param name="high">The High bound.</param>
        /// <returns>A gaussian random number with bounds: [low, high]</returns>
        public int GaussianRandom(int low, int high)
        {
            var u1 = 1D - _random.NextDouble();                                                      // (0, 1]
            var u2 = 1D - _random.NextDouble();                                                      // (0, 1]
            var randStdNormal = Math.Sqrt(-2D * Math.Log(u1)) * Math.Sin(2D * Math.PI * u2);   // random with a standard normal distribution
            var mean = low + (high - low) * _pokemonGeneratorConfig.Mean;
            var stdDeviation = (high - low) * _pokemonGeneratorConfig.StandardDeviation;
            var randNormal = mean + stdDeviation * randStdNormal;                              // random scaled with the standard deviation and translated with the mean
            return (int)Math.Max(Math.Min(randNormal, high), low);                             // clamp [low, high]
        }

        /// <summary>
        /// Chooses from a list of elements where each element is the relative probability of itself being chosen (Relative to all other elements). This may fail if there are elements with negative probabilties or if the list is empty.
        /// </summary>
        /// <param name="choices">A list of relative probabilities.</param>
        /// <returns>The index of the chosen element, null on failure.</returns>
        public int? ChooseWithProbability(IList<IChoice> choices)
        {
            var probChoices = choices.Select(pc => pc.Probability < 0 ? 0 : pc.Probability);
            var sum = probChoices.Sum();
            if (sum == 0) return 0;
            var norm = 1D / sum;
            var runningSum = 0D;
            var diceRoll = _random.NextDouble();
            return probChoices
                .Select(p => p * norm)                                // Normalize probabilities
                .Select(p => (runningSum += p))                       // Makes sure they all add up to 100 after being normalized
                .Select((p, index) => (probability: p, index: index))
                .First(t => diceRoll < t.probability).index;          // Choose from list with probabilities
        }
    }
}