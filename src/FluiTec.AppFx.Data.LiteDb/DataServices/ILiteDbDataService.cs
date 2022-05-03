using FluiTec.AppFx.Data.DataServices;
using LiteDB;

namespace FluiTec.AppFx.Data.LiteDb.DataServices;

/// <summary>   Interface for lite database data service. </summary>
public interface ILiteDbDataService : IDataService
{
    /// <summary>   Gets the database. </summary>
    /// <value> The database. </value>
    LiteDatabase Database { get; }
}