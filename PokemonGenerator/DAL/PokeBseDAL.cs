/// <summary>
/// Author: Justin Robb
/// Date: 8/30/2016
/// 
/// Description:
/// Generates a team of six Gen II pokemon for use in Pokemon Gold or Silver.
/// Built in order to supply Pokemon Stadium 2 with a better selection of Pokemon.
/// 
/// </summary>

namespace PokemonGenerator.DAL
{
    //using Modals;
    //using System;
    //using System.Collections.Generic;
    //using System.IO;
    //using System.Linq;
    //using System.Reflection;

    /// <summary>
    /// Not Used Anymore. See <see cref="SQLManager"/>.
    /// </summary>
    class PokeBseDAL // : IDisposable
    {
        //private string connectionString;
        //ThePokeBaseDataContext db;

        //public PokeBseDAL()
        //{
        //    Assembly ass = Assembly.GetExecutingAssembly();
        //    var dir = new FileInfo(ass.Location).Directory;
        //    var file = Path.Combine(dir.FullName, "ThePokeBase.mdf");
        //    connectionString = @"Data Source=JUSTINHOMEDESKT\SQLEXPRESS;Initial Catalog=ThePokeBase;Integrated Security=True";
        //    //"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"" + file + "\";Integrated Security=True";
        //    // @"Data Source=JUSTINHOMEDESKT\SQLEXPRESS;Initial Catalog=ThePokeBase;Integrated Security=True";
        //    db = new ThePokeBaseDataContext(connectionString);
        //}

        //public void Dispose()
        //{
        //    db.Dispose();
        //}

        //public List<int> GetPossiblePokemon(int level, Entropy entopy)
        //{
        //    var list = new List<int>();

        //    if (entopy < Entropy.High)
        //    {
        //        var query = db.GetPokemonByLevelStrict(level);
        //        query.ToList().ForEach((s) => list.Add(s.id));
        //    }
        //    else
        //    {
        //        var query = from mon in db.vwEvolutions select mon;
        //        query.ToList().ForEach((s) => list.Add(s.id));
        //    }

        //    return list;
        //}

        //public List<vwTM> GetTMBank()
        //{
        //    var query = db.vwTMs;

        //    return query.ToList();
        //}

        //public List<uspGetPokemonMoveSetResult> GetMovesForPokemon(int id, int level)
        //{
        //    var query = db.uspGetPokemonMoveSet(id, level);

        //    return query.ToList();
        //}


        //public List<vwBaseStat> GetTeamBaseStats(PokeList list) {
        //    var iList = list.Species.ToList();
        //    var query = from mon in db.vwBaseStats
        //                where iList.Contains((byte)mon.id)
        //                select mon;
        //    return query.ToList();
        //}

        //internal List<string> GetWeaknesses(string v)
        //{
        //    var list = new List<string>();
        //    var query = db.uspGetWeaknesses(v);
        //    query.ToList().ForEach((s) => list.Add(s.identifier));
        //    return list;
        //}
    }
}
