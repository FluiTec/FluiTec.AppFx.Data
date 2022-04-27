using System;
using FluiTec.AppFx.Data.Migration;
using Microsoft.EntityFrameworkCore;

namespace FluiTec.AppFx.Data.Ef.Extensions;

/// <summary>
/// A database context extensions.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Gets an option.
    /// </summary>
    ///
    /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
    ///                                                 the required range. </exception>
    ///
    /// <param name="sqlType">          Type of the SQL. </param>
    /// <param name="connectionString"> The connection string. </param>
    ///
    /// <returns>
    /// The option.
    /// </returns>
    public static DbContextOptions GetOption(SqlType sqlType, string connectionString)
    {
        switch (sqlType)
        {
            case SqlType.Mssql:
                return new DbContextOptionsBuilder().UseSqlServer(connectionString).Options;
            case SqlType.Mysql:
                return new DbContextOptionsBuilder().UseMySQL(connectionString).Options;
            case SqlType.Pgsql:
                return new DbContextOptionsBuilder().UseNpgsql(connectionString).Options;
            case SqlType.Sqlite:
                return new DbContextOptionsBuilder().UseSqlite(connectionString).Options;
            default:
                throw new ArgumentOutOfRangeException(nameof(sqlType), sqlType, null);
        }
    }
}