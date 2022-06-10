using System;
using FluiTec.AppFx.Data.Migration;
using RT.Comb;

namespace FluiTec.AppFx.Data.SequentialGuid;

/// <summary>
/// A comb sequential unique identifier generator.
/// </summary>
public class CombSequentialGuidGenerator : ISequentialGuidGenerator
{
    /// <summary>
    /// (Immutable) the provider.
    /// </summary>
    private readonly ICombProvider _provider;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public CombSequentialGuidGenerator() : this(SqlType.Mssql)
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="type"> The type. </param>
    public CombSequentialGuidGenerator(SqlType type)
    {
        _provider = type switch
        {
            SqlType.Pgsql => new PostgreSqlCombProvider(new UnixDateTimeStrategy()),
            _ => new SqlCombProvider(new UnixDateTimeStrategy())
        };
    }

    /// <summary>
    /// Generates a sequential unique identifier.
    /// </summary>
    ///
    /// <returns>
    /// The sequential unique identifier.
    /// </returns>
    public Guid GenerateSequentialGuid()
    {
        return _provider.Create();
    }
}