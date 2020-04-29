using System;
using System.Collections.Concurrent;
using LiteDB;

namespace FluiTec.AppFx.Data.LiteDb
{
    /// <summary>	A lite database database singleton. This class cannot be inherited. </summary>
    /// <remarks>
    ///     Used because LiteDb wont allow second connection to a database-file.
    ///     This singleton is unique for the appdomain - and limits the db-connection to appdomains user
    ///     - and thus enabling thread-safety.
    /// </remarks>
    internal sealed class LiteDbDatabaseSingleton
    {
        /// <summary>	The lazy initilizer. </summary>
        private static readonly Lazy<LiteDbDatabaseSingleton> Lazy =
            new Lazy<LiteDbDatabaseSingleton>(() => new LiteDbDatabaseSingleton());

        /// <summary>	Dictionary of databases. </summary>
        private readonly ConcurrentDictionary<string, LiteDatabase> _databaseDictionary;

        /// <summary>
        ///     Constructor that prevents a default instance of this class from being created.
        /// </summary>
        private LiteDbDatabaseSingleton()
        {
            _databaseDictionary = new ConcurrentDictionary<string, LiteDatabase>();
        }

        /// <summary>	The instance. </summary>
        public static LiteDbDatabaseSingleton Instance => Lazy.Value;

        /// <summary>	Gets a database. </summary>
        /// <exception cref="InvalidOperationException"> Thrown when the requested operation is invalid. </exception>
        /// <param name="dbFile">	The database file. </param>
        /// <returns>	The database. </returns>
        public static LiteDatabase GetDatabase(string dbFile)
        {
            if (Instance._databaseDictionary.ContainsKey(dbFile)) return Instance._databaseDictionary[dbFile];

            var added = Instance._databaseDictionary.TryAdd(dbFile, new LiteDatabase(dbFile));
            if (!added)
                throw new InvalidOperationException($"Could not add database for {dbFile}.");
            return Instance._databaseDictionary[dbFile];
        }
    }
}