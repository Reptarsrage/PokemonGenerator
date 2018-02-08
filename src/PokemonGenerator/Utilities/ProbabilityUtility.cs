using PokemonGenerator.Models.Configuration;
using PokemonGenerator.Models.Gernerator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGenerator.Utilities
{
    /// <summary>
    /// Uses statistics to provide random-ish choices
    /// </summary>
    public interface IProbabilityUtility
    {
        /// <summary>
        /// Chooses from a list of elements where each element is the relative probability of itself being chosen (Relative to all other elements). This may fail if there are elements with negative probabilties or if the list is empty.
        /// </summary>
        /// <param name="choices">A list of relative probabilities.</param>
        /// <returns>The index of the chosen element, null on failure.</returns>
        int? ChooseWithProbability(IList<IChoice> choices);

        /// <summary>
        /// Use Box-Muller transform to simulate a gaussian distribution 
        /// with a mean of <see cref="GeneratorConfig.StandardDeviation"/> 
        /// and a standard deviation of <see cref="GeneratorConfig.StandardDeviation"/>.
        /// </summary>
        /// <param name="low">The low bound</param>
        /// <param name="high">The High bound.</param>
        /// <returns>A gaussian random number with bounds: [low, high]</returns>
        int GaussianRandom(int low, int high);

        /// <summary>
        /// Use Box-Muller transform to simulate a gaussian distribution 
        /// with a mean of <see cref="GeneratorConfig.StandardDeviation"/> 
        /// and a standard deviation of <see cref="GeneratorConfig.StandardDeviation"/>.
        /// </summary>
        /// <param name="low">The low bound</param>
        /// <param name="high">The High bound.</param>
        /// <param name="skew">Skew [0-1] which wil influence the mean a bit.</param>
        /// <returns>A gaussian random number with bounds: [low, high]</returns>
        int GaussianRandomSkewed(int low, int high, double level);
    }

    /// <inheritdoc />
    public class ProbabilityUtility : IProbabilityUtility
    {
        private readonly Random _random;
        private readonly GeneratorConfig _generatorConfig;

        public ProbabilityUtility(Random random, GeneratorConfig generatorConfig)
        {
            _random = random;
            _generatorConfig = generatorConfig;
        }

        /// <inheritdoc />
        public int GaussianRandomSkewed(int low, int high, double skew)
        {
            if (skew > 1 || skew < 0)
            {
                throw new ArgumentException(nameof(skew));
            }

            var u1 = 1D - _random.NextDouble();                                                // (0, 1]
            var u2 = 1D - _random.NextDouble();                                                // (0, 1]
            var randStdNormal = Math.Sqrt(-2D * Math.Log(u1)) * Math.Sin(2D * Math.PI * u2);   // random with a standard normal distribution
            skew = skew * _generatorConfig.Skew * 2D - _generatorConfig.Skew;
            var mean = Math.Min(high, Math.Max(low, (double)(low + (high - low) * _generatorConfig.Mean + skew)));
            var stdDeviation = (high - low) * _generatorConfig.StandardDeviation;
            var randNormal = mean + stdDeviation * randStdNormal;                              // random scaled with the standard deviation and translated with the mean
            return (int)Math.Max(Math.Min(randNormal, high), low);                             // clamp [low, high]
        }

        /// <inheritdoc />
        public int GaussianRandom(int low, int high)
        {
            var u1 = 1D - _random.NextDouble();                                                // (0, 1]
            var u2 = 1D - _random.NextDouble();                                                // (0, 1]
            var randStdNormal = Math.Sqrt(-2D * Math.Log(u1)) * Math.Sin(2D * Math.PI * u2);   // random with a standard normal distribution
            var mean = low + (high - low) * _generatorConfig.Mean;
            var stdDeviation = (high - low) * _generatorConfig.StandardDeviation;
            var randNormal = mean + stdDeviation * randStdNormal;                              // random scaled with the standard deviation and translated with the mean
            return (int)Math.Max(Math.Min(randNormal, high), low);                             // clamp [low, high]
        }

        /// <inheritdoc />
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