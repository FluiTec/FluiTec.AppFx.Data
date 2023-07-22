using FluiTec.AppFx.Data.DataProviders;
using FluiTec.AppFx.Data.Sql.Enums;
using FluiTec.AppFx.Data.Sql.SqlBuilders;
using FluiTec.AppFx.Data.Sql.StatementBuilders;
using FluiTec.AppFx.Data.Sql.StatementProviders;

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

    /// <summary> Gets the SQL builder.</summary>
    /// <value> The SQL builder.</value>
    ISqlBuilder SqlBuilder { get; }
    
    /// <summary> Gets the statement provider.</summary>
    /// <value> The statement provider.</value>
    IStatementProvider StatementProvider { get; }
}