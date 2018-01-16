using NUnit.Framework;
using PokemonGenerator.Models;
using PokemonGenerator.Utilities;
using System;
using System.Linq;

namespace PokemonGenerator.Tests.Utility_Tests
{
    [TestFixture]
    public class ProbabilityUtilityTests
    {
        private Random random;
        private IProbabilityUtility probabilityUtility;
        private PokemonGeneratorConfig config;
        private const int iterations = 50000;
        private const double stdDeviation = 0.05;

        [SetUp]
        public void SetUp()
        {
            config = new PokemonGeneratorConfig();
            random = new Random("The cake is a lie".GetHashCode());
            probabilityUtility = new ProbabilityUtility(random, config);
        }

        [Test]
        [Category("Unit")]
        public void ChooseWithProbabilityOneChoice()
        {
            var elts = Enumerable.Range(0, 100).Select(i => new TestChoice(0) as IChoice).ToList();
            elts[25].Probability = 1;

            // Assert
            Assert.AreEqual(25, probabilityUtility.ChooseWithProbability(elts));
        }

        [Test]
        [Category("Unit")]
        public void ChooseWithProbabilityNoChoice()
        {
            var elts = Enumerable.Range(0, 100).Select(i => new TestChoice(0) as IChoice).ToList();

            // Assert
            Assert.AreEqual(0, probabilityUtility.ChooseWithProbability(elts));
        }

        [Test]
        [Category("Unit")]
        public void ChooseWithProbabilityTwoChoices()
        {
            var elts = Enumerable.Range(0, 100).Select(i => new TestChoice(0) as IChoice).ToList();
            elts[25].Probability = 0.8;
            elts[63].Probability = 0.2;
            var OneCount = 0;
            var TwoCount = 0;
            var result = 0;
            for (var i = 0; i < iterations; i++)
            {
                result = probabilityUtility.ChooseWithProbability(elts) ?? -1;
                if (result == 25)
                    OneCount++;
                else if (result == 63)
                    TwoCount++;
            }

            // Assert
            Assert.AreEqual(iterations, OneCount + TwoCount);
            Assert.LessOrEqual(Math.Abs((OneCount / (double)iterations) - 0.8), stdDeviation);
            Assert.LessOrEqual(Math.Abs((TwoCount / (double)iterations) - 0.2), stdDeviation);
        }

        [Test]
        [Category("Unit")]
        public void ChooseWithProbabilityTwoUnNormalizedChoices()
        {
            var elts = Enumerable.Range(0, 100).Select(i => new TestChoice(0) as IChoice).ToList();
            elts[25].Probability = 0.2;
            elts[63].Probability = 0.4;
            var OneCount = 0;
            var TwoCount = 0;
            var result = 0;
            for (var i = 0; i < iterations; i++)
            {
                result = probabilityUtility.ChooseWithProbability(elts) ?? -1;
                if (result == 25)
                    OneCount++;
                else if (result == 63)
                    TwoCount++;
            }

            // Assert
            Assert.AreEqual(iterations, OneCount + TwoCount);
            Assert.LessOrEqual(Math.Abs((TwoCount / (double)OneCount) - 2D), stdDeviation);
        }

        [Test]
        [Category("Unit")]
        public void ChooseWithProbabilityManyUnNormalizedChoices()
        {
            var elts = Enumerable.Range(0, 4).Select(i => new TestChoice(Math.Pow(1.1D, i)) as IChoice).ToList();
            var results = new int[elts.Count];
            var result = 0;
            for (var i = 0; i < iterations; i++)
            {
                result = probabilityUtility.ChooseWithProbability(elts) ?? -1;
                results[result]++;
            }

            // Assert
            for (var i = 1; i < results.Length; i++)
            {
                Assert.LessOrEqual(Math.Abs((results[i] / (double)results[i - 1]) - 1.1D), 2D * stdDeviation);
            }
        }

        [Test]
        [Category("Unit")]
        public void ChooseWithProbabilityManyNormalizedChoices()
        {
            var elts = Enumerable.Range(1, 4).Select(i => new TestChoice(i * 10) as IChoice).ToList();
            var results = new int[elts.Count];
            var result = 0;
            for (var i = 0; i < iterations; i++)
            {
                result = probabilityUtility.ChooseWithProbability(elts) ?? -1;
                results[result]++;
            }

            // Assert
            for (var i = 0; i < results.Length; i++)
            {
                Assert.LessOrEqual(Math.Abs((results[i] / (double)iterations) - (i + 1) / 10D), stdDeviation);
            }
        }

        [Test]
        [Category("Unit")]
        public void ChooseWithProbabilityManySpaghettiChoices()
        {
            var elts = Enumerable.Range(1, 2000).Select(i => new TestChoice(Math.Log(i, 2) + Math.Sin(i)) as IChoice).ToList();

            // Assert
            for (var i = 0; i < iterations; i++)
            {
                Assert.NotNull(probabilityUtility.ChooseWithProbability(elts));
            }
        }

        [Test]
        [Category("Unit")]
        public void ChooseWithProbabilityChoicesUnchanged()
        {
            var elts = Enumerable.Range(1, 2000).Select(i => new TestChoice(Math.Log(i, 2) + Math.Sin(i)) as IChoice).ToList();
            var oldElts = new IChoice[elts.Count];
            elts.CopyTo(oldElts);

            for (var i = 0; i < iterations; i++)
            {
                probabilityUtility.ChooseWithProbability(elts);
            }

            Assert.AreEqual(oldElts.ToList(), elts);
        }

        [Test]
        [Category("Unit")]
        [TestCase(0, 100)]
        [TestCase(20, 80)]
        [TestCase(30, 90)]
        [TestCase(67, 133)]
        public void GaussianRandomBasic(int low, int high)
        {
            var result = Enumerable.Range(0, iterations).Select(i => probabilityUtility.GaussianRandom(low, high));
            var resultMean = result.Average();
            var resultStdDeviation = Math.Sqrt(result.Select(i => Math.Pow(i - resultMean, 2D)).Average());

            var mean = low + (high - low) * config.Mean;
            var stdev = (high - low) * config.StandardDeviation;

            // Assert
            Assert.LessOrEqual(Math.Abs(resultMean - mean) / mean, stdDeviation, $"Mean Expected: {mean}, Actual: {resultMean}");
            Assert.LessOrEqual(Math.Abs(resultStdDeviation - stdev) / stdev, stdDeviation, $"Standard Deviation Expected: {stdev}, Actual: {resultStdDeviation}");
        }

        [Test]
        [Category("Unit")]
        [TestCase(-100, 99)]
        [TestCase(-20, 80)]
        [TestCase(-30, -10)]
        [TestCase(-67, 0)]
        public void GaussianRandomNegativeTest(int low, int high)
        {
            var result = Enumerable.Range(0, iterations).Select(i => probabilityUtility.GaussianRandom(low, high));
            var resultMean = result.Average();
            var resultStdDeviation = Math.Sqrt(result.Select(i => Math.Pow(i - resultMean, 2D)).Average());

            var mean = low + (high - low) * config.Mean;
            var stdev = (high - low) * config.StandardDeviation;

            // Assert
            Assert.LessOrEqual(Math.Abs(resultMean - mean) / mean, stdDeviation, $"Mean Expected: {mean}, Actual: {resultMean}");
            Assert.LessOrEqual(Math.Abs(resultStdDeviation - stdev) / stdev, stdDeviation, $"Standard Deviation Expected: {stdev}, Actual: {resultStdDeviation}");
        }

        [Test]
        [Category("Unit")]
        [TestCase(0, 100, 0.5, 0.2)]
        [TestCase(33, 66, 0.2, 0.1)]
        [TestCase(99, 30012, 0.33, 0.2)]
        [TestCase(-33, 0, 0.5, 0.08)]
        [TestCase(40, 4000, 0.90, 0.05)]
        public void GaussianRandomWithConfigTest(int low, int high, double meanConfig, double stdDeviationConfig)
        {
            config.Mean = meanConfig;
            config.StandardDeviation = stdDeviationConfig;
            probabilityUtility = new ProbabilityUtility(random, config);

            var result = Enumerable.Range(0, iterations).Select(i => probabilityUtility.GaussianRandom(low, high));
            var resultMean = result.Average();
            var resultStdDeviation = Math.Sqrt(result.Select(i => Math.Pow(i - resultMean, 2D)).Average());

            var mean = low + (high - low) * meanConfig;
            var stdev = (high - low) * stdDeviationConfig;

            // Assert
            Assert.LessOrEqual(Math.Abs(resultMean - mean) / mean, stdDeviation, $"Mean Expected: {mean}, Actual: {resultMean}");
            Assert.LessOrEqual(Math.Abs(resultStdDeviation - stdev) / stdev, stdDeviation, $"Standard Deviation Expected: {stdev}, Actual: {resultStdDeviation}");
        }


        [Test]
        [Category("Unit")]
        [TestCase(0, 100, 1)]
        [TestCase(0, 100, 50)]
        [TestCase(0, 100, 100)]
        [TestCase(20, 80, 75)]
        [TestCase(33, 67, 41)]
        public void GaussianRandomSkewedBasicTest(int low, int high, int level)
        {
            var result = Enumerable.Range(0, iterations).Select(i => probabilityUtility.GaussianRandomSkewed(low, high, level / 100D));
            var resultMean = result.Average();
            var resultStdDeviation = Math.Sqrt(result.Select(i => Math.Pow(i - resultMean, 2D)).Average());

            var skew = level / 100D * config.Skew * 2D - config.Skew;
            var mean = Math.Min(high, Math.Max(low, low + (high - low) * config.Mean + skew));
            var stdev = (high - low) * config.StandardDeviation;

            // Assert
            Assert.LessOrEqual(Math.Abs(resultMean - mean) / mean, stdDeviation, $"Mean Expected: {mean}, Actual: {resultMean}");
            Assert.LessOrEqual(Math.Abs(resultStdDeviation - stdev) / stdev, stdDeviation, $"Standard Deviation Expected: {stdev}, Actual: {resultStdDeviation}");
        }

        [Test]
        [Category("Unit")]
        [TestCase(-100, 99, 75)]
        [TestCase(-20, 80, 25)]
        [TestCase(-30, -10, 50)]
        [TestCase(-67, 0, 100)]
        public void GaussianRandomSkewedNegativeTest(int low, int high, int level)
        {
            var result = Enumerable.Range(0, iterations).Select(i => probabilityUtility.GaussianRandomSkewed(low, high, level / 100D));
            var resultMean = result.Average();
            var resultStdDeviation = Math.Sqrt(result.Select(i => Math.Pow(i - resultMean, 2D)).Average());

            var skew = level / 100D * config.Skew * 2D - config.Skew;
            var mean = Math.Min(high, Math.Max(low, low + (high - low) * config.Mean + skew));
            var stdev = (high - low) * config.StandardDeviation;

            // Assert
            Assert.LessOrEqual(Math.Abs(resultMean - mean) / mean, stdDeviation, $"Mean Expected: {mean}, Actual: {resultMean}");
            Assert.LessOrEqual(Math.Abs(resultStdDeviation - stdev) / stdev, stdDeviation, $"Standard Deviation Expected: {stdev}, Actual: {resultStdDeviation}");
        }

        [Test]
        [Category("Unit")]
        [TestCase(0, 100, 0.5, 0.2, 75)]
        [TestCase(33, 66, 0.2, 0.1, 25)]
        [TestCase(99, 30012, 0.33, 0.2, 50)]
        [TestCase(-33, 0, 0.5, 0.08, 100)]
        [TestCase(40, 4000, 0.90, 0.05, 1)]
        public void GaussianRandomSkewedWithConfigTest(int low, int high, double meanConfig, double stdDeviationConfig, int level)
        {
            config.Mean = meanConfig;
            config.StandardDeviation = stdDeviationConfig;
            probabilityUtility = new ProbabilityUtility(random, config);

            var result = Enumerable.Range(0, iterations).Select(i => probabilityUtility.GaussianRandomSkewed(low, high, level / 100D));
            var resultMean = result.Average();
            var resultStdDeviation = Math.Sqrt(result.Select(i => Math.Pow(i - resultMean, 2D)).Average());

            var skew = level / 100D * config.Skew * 2D - config.Skew;
            var mean = Math.Min(high, Math.Max(low, low + (high - low) * config.Mean + skew));
            var stdev = (high - low) * stdDeviationConfig;

            // Assert
            Assert.LessOrEqual(Math.Abs(resultMean - mean) / mean, stdDeviation, $"Mean Expected: {mean}, Actual: {resultMean}");
            Assert.LessOrEqual(Math.Abs(resultStdDeviation - stdev) / stdev, stdDeviation, $"Standard Deviation Expected: {stdev}, Actual: {resultStdDeviation}");
        }

        public class TestChoice : IChoice
        {
            public TestChoice(double probability) { Probability = probability; }

            public double Probability { get; set; }
        }
    }
}