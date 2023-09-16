using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.Exceptions;
using FluiTec.AppFx.Data.Sql.SqlBuilders;
using FluiTec.AppFx.Data.Sql.StatementProviders.EventArguments;

namespace FluiTec.AppFx.Data.Sql.StatementBuilders;

/// <summary>   A default statement builder. </summary>
public abstract class DefaultStatementBuilder : IStatementBuilder
{
    /// <summary> Event queue for all listeners interested in SqlProvided events.</summary>
    public event EventHandler<SqlProvidedEventArgs>? SqlProvided;

    /// <summary>   Specialized constructor for use only by derived class. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="sqlBuilder">   The SQL builder. </param>
    protected DefaultStatementBuilder(ISqlBuilder sqlBuilder)
    {
        SqlBuilder = sqlBuilder ?? throw new ArgumentNullException(nameof(sqlBuilder));
    }

    /// <summary>   Gets the SQL builder. </summary>
    /// <value> The SQL builder. </value>
    public ISqlBuilder SqlBuilder { get; }

    /// <summary> Executes the 'type SQL provided' action.</summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="sql">      The SQL. </param>
    /// <param name="schema">   The schema. </param>
    /// <param name="provider"> (Optional) The provider. </param>
    protected virtual void OnTypeSqlProvided(string sql, ITypeSchema schema, [CallerMemberName] string? provider = null)
    {
        if (provider == null)
            throw new ArgumentNullException(nameof(provider));

        SqlProvided?.Invoke(this, new TypeSqlProvidedEventArgs(sql, provider, schema));
    }

    /// <summary>   Gets all statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   all statement. </returns>
    public virtual string GetAllStatement(ITypeSchema typeSchema)
    {
        var sql = $"{SqlBuilder.Keywords.Select} " +
               $"{SqlBuilder.RenderList(typeSchema.MappedProperties.Select(SqlBuilder.RenderProperty))} " +
               $"{SqlBuilder.Keywords.From} " +
               $"{SqlBuilder.RenderTableName(typeSchema)}";
        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }

    /// <summary> Gets count statement.</summary>
    /// <param name="typeSchema"> The type schema. </param>
    /// <returns> The count statement.</returns>
    public string GetCountStatement(ITypeSchema typeSchema)
    {
        var sql = $"{SqlBuilder.Keywords.Select} " +
                  $"{SqlBuilder.Keywords.CountAllExpression} " +
                  $"{SqlBuilder.Keywords.From} " +
                  $"{SqlBuilder.RenderTableName(typeSchema)}";
        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }

    /// <summary> Gets paging statement.</summary>
    /// <param name="typeSchema">        The type schema. </param>
    /// <param name="skipParameterName"> (Optional) Name of the skip parameter. </param>
    /// <param name="takeParameterName"> (Optional) Name of the take parameter. </param>
    /// <returns> The paging statement.</returns>
    public string GetPagingStatement(ITypeSchema typeSchema, string skipParameterName,
        string takeParameterName)
    {
        var sql = $"{SqlBuilder.Keywords.Select} " +
                  $"{SqlBuilder.RenderList(typeSchema.MappedProperties.Select(SqlBuilder.RenderProperty))} " +
                  $"{SqlBuilder.Keywords.From} " +
                  $"{SqlBuilder.RenderTableName(typeSchema)} " +
                  $"{SqlBuilder.Keywords.OrderByExpression} " +
                  $"{SqlBuilder.RenderProperty(typeSchema.MappedProperties.First())} " +
                  $"{SqlBuilder.Keywords.AscendingExpression} " +
                  $"{SqlBuilder.RenderOffsetParameter(skipParameterName)} " +
                  $"{SqlBuilder.RenderFetchNextParameter(takeParameterName)}";
        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }

    /// <summary>   Validates the key parameters. </summary>
    /// <param name="entityKeys">       The entity keys. </param>
    /// <param name="parameterKeys">    The parameter keys. </param>
    protected void ValidateKeyParameters(IEnumerable<IKeyPropertySchema> entityKeys,
        IDictionary<string, object> parameterKeys)
    {
        var keyPropertySchemata = entityKeys as IKeyPropertySchema[] ?? entityKeys.ToArray();

        if (keyPropertySchemata.Length != parameterKeys.Count)
            throw new KeyParameterMismatchException(keyPropertySchemata, parameterKeys);

        foreach (var k in parameterKeys)
        {
            _ = keyPropertySchemata.SingleOrDefault(kp => kp.Name.ColumnName == k.Key) ?? throw new KeyParameterMismatchException(keyPropertySchemata, parameterKeys);
        }
    }

    /// <summary>   Gets select by key statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <param name="keys">          The keys. </param>
    /// <returns>   The select by key statement. </returns>
    public string GetSelectByKeyStatement(ITypeSchema typeSchema, IDictionary<string, object> keys)
    {
        var keyProps = typeSchema.KeyProperties;
        ValidateKeyParameters(keyProps, keys);
        var sql = $"{SqlBuilder.Keywords.Select} " +
                  $"{SqlBuilder.RenderList(typeSchema.MappedProperties.Select(SqlBuilder.RenderProperty))} " +
                  $"{SqlBuilder.Keywords.From} " +
                  $"{SqlBuilder.RenderTableName(typeSchema)} " +
                  $"{SqlBuilder.Keywords.Where} " +
                  $"{SqlBuilder.RenderList(keys
                      .Select(k => SqlBuilder.RenderPropertyParameterComparison(keyProps.Single(kp => kp.Name.ColumnName == k.Key), SqlBuilder.Keywords.CompareEqualsOperator)))}"
                  ;

        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }
}