using NUnit.Framework;
using PokemonGenerator.DAL;
using PokemonGenerator.Models;
using System.Linq;

namespace PokemonGenerator.Tests
{
    [TestFixture]
    public class PokemonDATests
    {
        [Test]
        [Category("Integration")]
        public void GetPossiblePokemonTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var pokemon = da.GetPossiblePokemon(100, Enumerations.Entropy.Low);
            Assert.IsNotNull(pokemon);
            Assert.Greater(pokemon.Count(), 0);
        }

        [Test]
        [Category("Integration")]
        public void GetPossiblePokemonValuesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var pokemon = da.GetPossiblePokemon(100, Enumerations.Entropy.Low);
            pokemon.ToList().ForEach(Assert.NotZero);
        }

        [Test]
        [Category("Integration")]
        public void GetMovesForPokemonTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var moves = da.GetMovesForPokemon(62, 100);
            Assert.IsNotNull(moves);
            Assert.Greater(moves.Count(), 0);
        }

        [Test]
        [Category("Integration")]
        public void GetMovesForPokemonValuesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var moves = da.GetMovesForPokemon(62, 100);
            moves.ToList().ForEach(AssertPokemonMoveSetResult);
        }

        [Test]
        [Category("Integration")]
        public void GetRandomMovesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var moves = da.GetRandomMoves(0, 150);
            Assert.IsNotNull(moves);
            Assert.Greater(moves.Count(), 0);
        }

        [Test]
        [Category("Integration")]
        public void GetRandomMovesValuesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var moves = da.GetRandomMoves(0, 150);
            moves.ToList().ForEach(AssertPokemonMoveSetResult);
        }

        [Test]
        [Category("Integration")]
        public void GetTeamBaseStatsTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var baseStats = da.GetTeamBaseStats(new PokeList(4)
            {
                Species = new byte[] { 0, 1, 2, 3 }
            });
            Assert.IsNotNull(baseStats);
            Assert.Greater(baseStats.Count(), 0);
        }

        [Test]
        [Category("Integration")]
        public void GetTeamBaseStatsValuesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var baseStats = da.GetTeamBaseStats(new PokeList(4)
            {
                Species = new byte[] { 0, 1, 2, 3 }
            });
            foreach (var stat in baseStats)
            {
                Assert.NotZero(stat.Id);
                Assert.NotZero(stat.Hp);
                Assert.NotZero(stat.Attack);
                Assert.NotZero(stat.Defense);
                Assert.NotZero(stat.SpAttack);
                Assert.NotZero(stat.SpDefense);
                Assert.NotZero(stat.Speed);

                Assert.IsNotNull(stat.GrowthRate);
                Assert.IsNotEmpty(stat.GrowthRate);

                Assert.IsNotNull(stat.Identifier);
                Assert.IsNotEmpty(stat.Identifier);

                Assert.IsNotNull(stat.Types);
                Assert.IsNotEmpty(stat.Types);
            }
        }

        [Test]
        [Category("Integration")]
        public void GetTMsTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var tms = da.GetTMs();
            Assert.IsNotNull(tms);
            Assert.Greater(tms.Count(), 0);
        }

        [Test]
        [Category("Integration")]
        public void GetTMsValueTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var tms = da.GetTMs();
            tms.ToList().ForEach(Assert.NotZero);
        }

        [Test]
        [Category("Integration")]
        public void GetWeaknessesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var weaknesses = da.GetWeaknesses("fire");
            Assert.IsNotNull(weaknesses);
            Assert.Greater(weaknesses.Count(), 0);
        }

        [Test]
        [Category("Integration")]
        public void GetWeaknessesValuesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var weaknesses = da.GetWeaknesses("fire");
            weaknesses.ToList().ForEach(Assert.IsNotNull);
            weaknesses.ToList().ForEach(Assert.IsNotEmpty);
        }

        private void AssertPokemonMoveSetResult(PokemonMoveSetResult move)
        {
            Assert.LessOrEqual(move.Level, 100, "Level");
            Assert.GreaterOrEqual(move.Level, 0, "Level");
            Assert.NotZero(move.MoveId, "Move Id");
            Assert.GreaterOrEqual((int)(move?.Power ?? 0), 0, "Power");
            Assert.GreaterOrEqual((int)(move?.Pp ?? 0), 0, "PP");

            Assert.IsNotNull(move.MoveName, "Move Name");
            Assert.IsNotEmpty(move.MoveName, "Move Name");

            Assert.IsNotNull(move.Type, "Move Type");
            Assert.IsNotEmpty(move.Type, "Move Type");

            // damageType can be null when move does no damage
            Assert.IsNotEmpty(move.DamageType, "Move Damage Type");

            // learnType can be null
            Assert.IsNotEmpty(move.LearnType, "Move Learn Type");

            Assert.IsNotNull(move.Effect, "Move Effect");
            Assert.IsNotEmpty(move.Effect, "Move Effect");
        }
    }
}