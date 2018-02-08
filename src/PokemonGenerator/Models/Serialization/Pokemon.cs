using System.Collections.Generic;
using System.Text;

namespace PokemonGenerator.Models.Serialization
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    public class Pokemon
    {
        public byte SpeciesId; // 1
        public byte HeldItem; // 1
        public byte MoveIndex1; // 1
        public byte MoveIndex2; // 1
        public byte MoveIndex3; // 1
        public byte MoveIndex4; // 1
        public ushort TrainerId; // 2
        public uint Experience; // 3

        public ushort HitPointsEV; // 2
        public ushort AttackEV; // 2
        public ushort DefenseEV; // 2
        public ushort SpeedEV; // 2
        public ushort SpecialEV; // 2

        public byte AttackIV; // 4 bits        }
        public byte DefenseIV; // 4 bits       }
        public byte SpeedIV; // 4 bits         }
        public byte SpecialIV; // 4 bits       } >> 2

        public byte Move1PowerPointsUps; // 2 bits              }
        public byte Move1PowerPointsCurrent; // 6 bits          }  >> 1

        public byte Move2PowerPointsUps; // 2 bits              }
        public byte Move2PowerPointsCurrent; // 6 bits          }  >> 1

        public byte Move3PowerPointsUps; // 2 bits              }
        public byte Move3PowerPointsCurrent; // 6 bits          }  >> 1

        public byte Move4PowerPointsUps; // 2 bits              }
        public byte Move4PowerPointsCurrent; // 6 bits          }  >> 1

        public byte Friendship; // 1

        public byte PokerusStrain; // 4 bits       }
        public byte PokerusDuration; // 4 bits     }  >> 1

        public byte CaughtTime; // 2 bits            }
        public byte CaughtLevel; // 6 bits           }
        public byte OTGender; // 1 bits   }
        public byte CaughtLocation; // 7 bits        }  >> 2

        public byte Level; // 1

        // 32 BYTES - the following are only present when pokemon is outside a box

        public byte Status; // 1
        public byte Unused; // 1
        public ushort CurrentHp; // 2
        public ushort MaxHp; // 2
        public ushort Attack; // 2
        public ushort Defense; // 2
        public ushort Speed; // 2
        public ushort SpAttack; // 2
        public ushort SpDefense; // 2

        // 48 TOTAL BYTES (1 unused)

        // Additional Data
        public string OTName;
        public string Name;
        public IList<string> Types;
        public string Move1Name;
        public string Move2Name;
        public string Move3Name;
        public string Move4Name;

        /// <summary>
        /// Prettry prints the Pokemon contents
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"\n {(string.IsNullOrEmpty(Name) ? SpeciesId.ToString() : Name)}");
            builder.Append($"\n lvl {Level}  {(OTGender == 1 ? "Female" : "Male")}");
            builder.Append($"\n heldItem: {HeldItem}");
            builder.Append($"\n move 1: {(string.IsNullOrEmpty(Move1Name) ? MoveIndex1.ToString() : Move1Name)}\t pp {Move1PowerPointsCurrent} (up {Move1PowerPointsUps})");
            builder.Append($"\n move 2: {(string.IsNullOrEmpty(Move2Name) ? MoveIndex2.ToString() : Move2Name)}\t pp {Move2PowerPointsCurrent} (up {Move2PowerPointsUps})");
            builder.Append($"\n move 3: {(string.IsNullOrEmpty(Move3Name) ? MoveIndex3.ToString() : Move3Name)}\t pp {Move3PowerPointsCurrent} (up {Move3PowerPointsUps})");
            builder.Append($"\n move 4: {(string.IsNullOrEmpty(Move4Name) ? MoveIndex4.ToString() : Move4Name)}\t pp {Move4PowerPointsCurrent} (up {Move4PowerPointsUps})");
            builder.Append($"\n trainerID: {TrainerId}");
            builder.Append($"\n hpEV {HitPointsEV}");
            builder.Append($"\n attackEV {AttackEV}\n attackIV {AttackIV}");
            builder.Append($"\n defenseEV {DefenseEV}\n defenseIV {DefenseIV}");
            builder.Append($"\n speedEV {SpeedEV}\n speedIV {SpeedIV}");
            builder.Append($"\n specialEV {SpecialEV}\n specialIV {SpecialIV}");

            if (this.MaxHp > 0)
            {
                builder.Append($"\n status: {Status}");
                builder.Append($"\n currentHp: {CurrentHp}");
                builder.Append($"\n maxHp: {MaxHp}");
                builder.Append($"\n attack: {Attack}");
                builder.Append($"\n defense: {Defense}");
                builder.Append($"\n speed: {Speed}");
                builder.Append($"\n spAttack: {SpAttack}");
                builder.Append($"\n spDefense: {SpDefense}");
            }

            return builder.ToString();
        }
    }
}