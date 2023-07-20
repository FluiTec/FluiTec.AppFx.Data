using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.Sql.Enums;

namespace FluiTec.AppFx.Data.Dapper.Providers;

/// <summary>   Interface for dapper data provider. </summary>
public interface IDapperDataProvider : IDataProvider
{
    /// <summary>   Gets the connection factory. </summary>
    /// <value> The connection factory. </value>
    IConnectionFactory ConnectionFactory { get; }

    /// <summary>   Gets the connection string. </summary>
    /// <value> The connection string. </value>
    string ConnectionString { get; }

    /// <summary>   Gets the type of the SQL. </summary>
    /// <value> The type of the SQL. </value>
    SqlType SqlType { get; }
}