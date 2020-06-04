using FluentMigrator.Runner.VersionTableInfo;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Migration;

namespace FluiTec.AppFx.Data.Dapper.DataServices
{
    /// <summary>   Interface for dapper data service. </summary>
    public interface IDapperDataService : IDataService
    {
        /// <summary>   Gets the connection factory. </summary>
        /// <value> The connection factory. </value>
        IConnectionFactory ConnectionFactory { get; }

        /// <summary>   Gets the connection string. </summary>
        /// <value> The connection string. </value>
        string ConnectionString { get; }

        /// <summary>   Gets information describing the meta. </summary>
        /// <value> Information describing the meta. </value>
        // ReSharper disable once UnusedMemberInSuper.Global
        IVersionTableMetaData MetaData { get; }

        /// <summary>   Gets the schema. </summary>
        /// <value> The schema. </value>
        // ReSharper disable once UnusedMemberInSuper.Global
        string Schema { get; }

        /// <summary>   Gets the type of the SQL. </summary>
        /// <value> The type of the SQL. </value>
        // ReSharper disable once UnusedMemberInSuper.Global
        SqlType SqlType { get; }
    }
}