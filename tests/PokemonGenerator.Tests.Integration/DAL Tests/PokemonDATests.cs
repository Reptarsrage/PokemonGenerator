using PokemonGenerator.DAL;
using PokemonGenerator.Models;
using System.Linq;
using Xunit;

namespace PokemonGenerator.Tests
{
    public class PokemonDATests
    {
        [Fact]
        public void GetPossiblePokemonTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var pokemon = da.GetPossiblePokemon(100, Enumerations.Entropy.Low);
            Assert.NotNull(pokemon);
            Assert.True(pokemon.Count() > 0, "Pokemon has at least one pokemon");
        }

        [Fact]
        public void GetPossiblePokemonValuesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var pokemon = da.GetPossiblePokemon(100, Enumerations.Entropy.Low);
            pokemon.ToList().ForEach(t => Assert.True(t != 0, "Not zero"));
        }

        [Fact]
        public void GetMovesForPokemonTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var moves = da.GetMovesForPokemon(62, 100);
            Assert.NotNull(moves);
            Assert.True(moves.Count() > 0, "Moves has at least one move");
        }

        [Fact]
        public void GetMovesForPokemonValuesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var moves = da.GetMovesForPokemon(62, 100);
            moves.ToList().ForEach(AssertPokemonMoveSetResult);
        }

        [Fact]
        public void GetRandomMovesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var moves = da.GetRandomMoves(0, 150);
            Assert.NotNull(moves);
            Assert.True(moves.Count() > 0, "Moves has at least one move");
        }

        [Fact]
        public void GetRandomMovesValuesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var moves = da.GetRandomMoves(0, 150);
            moves.ToList().ForEach(AssertPokemonMoveSetResult);
        }

        [Fact]
        public void GetTeamBaseStatsTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var baseStats = da.GetTeamBaseStats(new PokeList(4)
            {
                Species = new byte[] { 0, 1, 2, 3 }
            });
            Assert.NotNull(baseStats);
            Assert.True(baseStats.Count() > 0, "Stats has at least one stat");
        }

        [Fact]
        public void GetTeamBaseStatsValuesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var baseStats = da.GetTeamBaseStats(new PokeList(4)
            {
                Species = new byte[] { 0, 1, 2, 3 }
            });
            foreach (var stat in baseStats)
            {
                Assert.True(stat.Id != 0, "Id not zero");
                Assert.True(stat.Hp != 0, "Hp not zero");
                Assert.True(stat.Attack != 0, "Attack not zero");
                Assert.True(stat.Defense != 0, "Defense not zero");
                Assert.True(stat.SpAttack != 0, "SpAttack not zero");
                Assert.True(stat.SpDefense != 0, "SpDefense not zero");
                Assert.True(stat.Speed != 0, "Speed not zero");

                Assert.NotNull(stat.GrowthRate);
                Assert.NotEmpty(stat.GrowthRate);

                Assert.NotNull(stat.Identifier);
                Assert.NotEmpty(stat.Identifier);

                Assert.NotNull(stat.Types);
                Assert.NotEmpty(stat.Types);
            }
        }

        [Fact]
        public void GetTMsTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var tms = da.GetTMs();
            Assert.NotNull(tms);
            Assert.True(tms.Count() > 0, "At least one TM");
        }

        [Fact]
        public void GetTMsValueTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var tms = da.GetTMs();
            tms.ToList().ForEach(tm => Assert.True(tm != 0, "TM not zero"));
        }

        [Fact]
        public void GetWeaknessesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var weaknesses = da.GetWeaknesses("fire");
            Assert.NotNull(weaknesses);
            Assert.True(weaknesses.Count() > 0, "At least one weakness");
        }

        [Fact]
        public void GetWeaknessesValuesTest()
        {
            var da = new PokemonDA("ThePokeBase");
            var weaknesses = da.GetWeaknesses("fire");
            weaknesses.ToList().ForEach(Assert.NotNull);
            weaknesses.ToList().ForEach(Assert.NotEmpty);
        }

        private void AssertPokemonMoveSetResult(PokemonMoveSetResult move)
        {
            Assert.True(move.Level <= 100, "Level <= 100");
            Assert.True(move.Level >= 0, "Level >= 0");
            Assert.True(move.MoveId != 0, "Move Id");
            Assert.True((int)(move?.Power ?? 0) >= 0, "Power");
            Assert.True((int)(move?.Pp ?? 0) >= 0, "PP");

            Assert.NotNull(move.MoveName);
            Assert.NotEmpty(move.MoveName);

            Assert.NotNull(move.Type);
            Assert.NotEmpty(move.Type);

            // damageType can be null when move does no damage
            Assert.True(move.DamageType == null || !string.IsNullOrWhiteSpace(move.DamageType), "Damage Type is valid");

            // learnType can be null
            Assert.True(move.LearnType == null || !string.IsNullOrWhiteSpace(move.LearnType), "LearnType Type is valid");

            Assert.NotNull(move.Effect);
            Assert.NotEmpty(move.Effect);
        }
    }
}