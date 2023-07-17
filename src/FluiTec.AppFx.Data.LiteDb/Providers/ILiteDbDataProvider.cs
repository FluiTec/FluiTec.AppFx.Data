using FluiTec.AppFx.Data.DataProviders;
using LiteDB;

namespace FluiTec.AppFx.Data.LiteDb.Providers;

/// <summary>   Interface for lite database data provider. </summary>
public interface ILiteDbDataProvider : IDataProvider
{
    /// <summary>   Gets the database. </summary>
    /// <value> The database. </value>
    LiteDatabase Database { get; }
}