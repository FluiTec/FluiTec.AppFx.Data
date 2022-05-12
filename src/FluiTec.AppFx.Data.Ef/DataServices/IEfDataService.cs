using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Migration;

namespace FluiTec.AppFx.Data.Ef.DataServices;

/// <summary>
///     Interface for ef data service.
/// </summary>
public interface IEfDataService : IDataService
{
    /// <summary>
    ///     Gets the type of the SQL.
    /// </summary>
    /// <value>
    ///     The type of the SQL.
    /// </value>
    public SqlType SqlType { get; }

    /// <summary>
    ///     Gets the connection string.
    /// </summary>
    /// <value>
    ///     The connection string.
    /// </value>
    string ConnectionString { get; }

    /// <summary>
    ///     Gets the context.
    /// </summary>
    /// <returns>
    ///     The context.
    /// </returns>
    IDynamicDbContext GetContext();
}