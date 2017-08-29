using System.Collections.Generic;
using System.Text;

namespace PokemonGenerator.Models
{
    /// <summary>
    /// A modal used to store and serialize/deserialize information.
    /// </summary>
    internal class Pokemon
    {
        public byte species; // 1

        public byte heldItem; // 1

        public byte moveIndex1; // 1
        public byte moveIndex2; // 1
        public byte moveIndex3; // 1
        public byte moveIndex4; // 1

        public ushort trainerID; // 2

        public uint experience; // 3

        public ushort hpEV; // 2
        public ushort attackEV; // 2
        public ushort defenseEV; // 2
        public ushort speedEV; // 2
        public ushort specialEV; // 2

        public byte attackIV; // 4 bits        }
        public byte defenseIV; // 4 bits       }
        public byte speedIV; // 4 bits         }
        public byte specialIV; // 4 bits       } >> 2

        public byte ppUps1; // 2 bits             }
        public byte currentPP1; // 6 bits         }  >> 1

        public byte ppUps2; // 2 bits              }
        public byte currentPP2; // 6 bits          }  >> 1

        public byte ppUps3; // 2 bits               }
        public byte currentPP3; // 6 bits           }  >> 1

        public byte ppUps4; // 2 bits              }
        public byte currentPP4; // 6 bits          }  >> 1

        public byte friendship; // 1

        public byte pokerusStrain; // 4 bits       }
        public byte pokerusDuration; // 4 bits     }  >> 1

        public byte caughtTime; // 2 bits            }
        public byte caughtLevel; // 6 bits           }
        public byte OTGender; // 1 bits   }
        public byte caughtLocation; // 7 bits        }  >> 2

        public byte level; // 1

        // 32 BYTES - the following are only present when pokemon is outside a box

        public byte status; // 1
        public byte unused; // 1
        public ushort currentHp; // 2
        public ushort maxHp; // 2
        public ushort attack; // 2
        public ushort defense; // 2
        public ushort speed; // 2
        public ushort spAttack; // 2
        public ushort spDefense; // 2

        // 48 TOTAL BYTES (1 unused)

        // Additional Data
        public string OTName;
        public string Name;
        public List<string> Types;
        public string MoveName1;
        public string MoveName2;
        public string MoveName3;
        public string MoveName4;

        /// <summary>
        /// Prettry prints the Pokemon contents
        /// </summary>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"\n {(string.IsNullOrEmpty(Name) ? species.ToString() : Name)}");
            builder.Append($"\n lvl {level}  {(OTGender == 1 ? "Female" : "Male")}");
            builder.Append($"\n heldItem: {heldItem}");
            builder.Append($"\n move 1: {(string.IsNullOrEmpty(MoveName1) ? moveIndex1.ToString() : MoveName1)}\t pp {currentPP1} (up {ppUps1})");
            builder.Append($"\n move 2: {(string.IsNullOrEmpty(MoveName2) ? moveIndex2.ToString() : MoveName2)}\t pp {currentPP2} (up {ppUps2})");
            builder.Append($"\n move 3: {(string.IsNullOrEmpty(MoveName3) ? moveIndex3.ToString() : MoveName3)}\t pp {currentPP3} (up {ppUps3})");
            builder.Append($"\n move 4: {(string.IsNullOrEmpty(MoveName4) ? moveIndex4.ToString() : MoveName4)}\t pp {currentPP4} (up {ppUps4})");
            builder.Append($"\n trainerID: {trainerID}");
            builder.Append($"\n hpEV {hpEV}");
            builder.Append($"\n attackEV {attackEV}\n attackIV {attackIV}");
            builder.Append($"\n defenseEV {defenseEV}\n defenseIV {defenseIV}");
            builder.Append($"\n speedEV {speedEV}\n speedIV {speedIV}");
            builder.Append($"\n specialEV {specialEV}\n specialIV {specialIV}");

            if (this.maxHp > 0)
            {
                builder.Append($"\n status: {status}");
                builder.Append($"\n currentHp: {currentHp}");
                builder.Append($"\n maxHp: {maxHp}");
                builder.Append($"\n attack: {attack}");
                builder.Append($"\n defense: {defense}");
                builder.Append($"\n speed: {speed}");
                builder.Append($"\n spAttack: {spAttack}");
                builder.Append($"\n spDefense: {spDefense}");
            }

            return builder.ToString();
        }
    }
}