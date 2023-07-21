using System;
using System.Linq;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.SqlBuilders;

namespace FluiTec.AppFx.Data.Sql.StatementBuilders;

/// <summary>   A default statement builder. </summary>
public abstract class DefaultStatementBuilder : IStatementBuilder
{
    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="sqlBuilder">   The SQL builder. </param>
    protected DefaultStatementBuilder(ISqlBuilder sqlBuilder)
    {
        SqlBuilder = sqlBuilder ?? throw new ArgumentNullException(nameof(sqlBuilder));
    }

    /// <summary>   Gets the SQL builder. </summary>
    /// <value> The SQL builder. </value>
    public ISqlBuilder SqlBuilder { get; }

    /// <summary>   Gets all statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   all statement. </returns>
    public virtual string GetAllStatement(ITypeSchema typeSchema)
    {
        return $"{SqlBuilder.Keywords.Select} " +
               $"{SqlBuilder.RenderList(typeSchema.MappedProperties.Select(SqlBuilder.RenderProperty))}" +
               $"{SqlBuilder.Keywords.From} " +
               $"{SqlBuilder.RenderTableName(typeSchema)}";
    }
}