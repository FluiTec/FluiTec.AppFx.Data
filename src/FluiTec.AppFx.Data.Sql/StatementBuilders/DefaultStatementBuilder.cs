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

    /// <summary> Event queue for all listeners interested in SqlProvided events.</summary>
    public event EventHandler<SqlProvidedEventArgs>? SqlProvided;

    /// <summary>   Gets the SQL builder. </summary>
    /// <value> The SQL builder. </value>
    public ISqlBuilder SqlBuilder { get; }

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
    public virtual string GetCountStatement(ITypeSchema typeSchema)
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
    public virtual string GetPagingStatement(ITypeSchema typeSchema, string skipParameterName,
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

    /// <summary>   Gets select by key statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <param name="keys">          The keys. </param>
    /// <returns>   The select by key statement. </returns>
    public virtual string GetSelectByKeyStatement(ITypeSchema typeSchema, IDictionary<string, object> keys)
    {
        var keyProps = typeSchema.KeyProperties;
        ValidateKeyParameters(keyProps, keys);
        var sql = $"{SqlBuilder.Keywords.Select} " +
                  $"{SqlBuilder.RenderList(typeSchema.MappedProperties.Select(SqlBuilder.RenderProperty))} " +
                  $"{SqlBuilder.Keywords.From} " +
                  $"{SqlBuilder.RenderTableName(typeSchema)} " +
                  $"{SqlBuilder.Keywords.Where} " +
                  $"{SqlBuilder.RenderList(keys
                      .Select(k => SqlBuilder
                          .RenderPropertyParameterComparison(keyProps.Single(kp => kp.Name.ColumnName == k.Key), SqlBuilder.Keywords.CompareEqualsOperator)))}"
            ;

        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }

    /// <summary>   Gets insert single statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert single statement. </returns>
    public virtual string GetInsertSingleStatement(ITypeSchema typeSchema)
    {
        var columns = typeSchema.MappedProperties.ToList();

        var sql = $"{SqlBuilder.Keywords.Insert} " +
                  $"{SqlBuilder.Keywords.Into} " +
                  $"{SqlBuilder.RenderTableName(typeSchema)} " +
                  $"({SqlBuilder.RenderList(columns.Select(SqlBuilder.RenderProperty))}) " +
                  $"{SqlBuilder.Keywords.Values} " +
                  $"({SqlBuilder.RenderList(columns.Select(c => SqlBuilder.RenderParameter(SqlBuilder.CreateParameterName(c))))})";

        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }

    /// <summary>   Gets retrieve automatic insert key statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The retrieve automatic insert key statement. </returns>
    public abstract string GetRetrieveAutoInsertKeyStatement(ITypeSchema typeSchema);

    /// <summary>   Gets insert single automatic statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert single automatic statement. </returns>
    public virtual string GetInsertSingleAutoStatement(ITypeSchema typeSchema)
    {
        var columns = typeSchema.MappedProperties.Except(new[] { typeSchema.IdentityKey! }).ToList();

        var sql = $"{SqlBuilder.Keywords.Insert} " +
                  $"{SqlBuilder.Keywords.Into} " +
                  $"{SqlBuilder.RenderTableName(typeSchema)} " +
                  $"({SqlBuilder.RenderList(columns.Select(SqlBuilder.RenderProperty))}) " +
                  $"{SqlBuilder.Keywords.Values} " +
                  $"({SqlBuilder.RenderList(columns.Select(c => SqlBuilder.RenderParameter(SqlBuilder.CreateParameterName(c))))}) " +
                  $"{SqlBuilder.Keywords.CommandSeparator} {GetRetrieveAutoInsertKeyStatement(typeSchema)}";

        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }

    /// <summary>   Gets insert multiple statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert multiple statement. </returns>
    public virtual string GetInsertMultipleStatement(ITypeSchema typeSchema)
    {
        return GetInsertSingleStatement(typeSchema);
    }

    /// <summary>   Gets insert multiple automatic statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert multiple automatic statement. </returns>
    public virtual string GetInsertMultipleAutoStatement(ITypeSchema typeSchema)
    {
        var columns = typeSchema.MappedProperties.Except(new[] { typeSchema.IdentityKey! }).ToList();

        var sql = $"{SqlBuilder.Keywords.Insert} " +
                  $"{SqlBuilder.Keywords.Into} " +
                  $"{SqlBuilder.RenderTableName(typeSchema)} " +
                  $"({SqlBuilder.RenderList(columns.Select(SqlBuilder.RenderProperty))}) " +
                  $"{SqlBuilder.Keywords.Values} " +
                  $"({SqlBuilder.RenderList(columns.Select(c => SqlBuilder.RenderParameter(SqlBuilder.CreateParameterName(c))))})";

        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }

    /// <summary>   Gets update statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The update statement. </returns>
    public string GetUpdateStatement(ITypeSchema typeSchema)
    {
        var sql = $"{SqlBuilder.Keywords.Update} " +
                  $"{SqlBuilder.RenderTableName(typeSchema)} " +
                  $"{SqlBuilder.Keywords.Set} " +
                  $"{SqlBuilder
                      .RenderList(typeSchema.MappedProperties.Except(typeSchema.KeyProperties)
                          .Select(p => $"{SqlBuilder
                              .RenderPropertyAssignment(p, SqlBuilder.Keywords.AssignEqualsOperator)}"))} " +
                  $"{SqlBuilder.Keywords.Where} " +
                  $"{SqlBuilder
                      .RenderJoinExpressions(typeSchema.KeyProperties
                          .Select(kp => $"{SqlBuilder
                              .RenderPropertyParameterComparison(kp, SqlBuilder.Keywords.CompareEqualsOperator)}"), $" {SqlBuilder.Keywords.And}")}";

        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }

    /// <summary>   Gets delete statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The delete statement. </returns>
    public string GetDeleteStatement(ITypeSchema typeSchema)
    {
        var sql = $"{SqlBuilder.Keywords.Delete} " +
                  $"{SqlBuilder.Keywords.From} " +
                  $"{SqlBuilder.RenderTableName(typeSchema)} " +
                  $"{SqlBuilder.Keywords.Where} " +
                  $"{SqlBuilder
                      .RenderJoinExpressions(typeSchema.KeyProperties
                          .Select(kp => $"{SqlBuilder
                              .RenderPropertyParameterComparison(kp, SqlBuilder.Keywords.CompareEqualsOperator)}"), $" {SqlBuilder.Keywords.And}")}";

        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }

    /// <summary>   Gets delete statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <param name="keys">         The keys. </param>
    /// <returns>   The delete statement. </returns>
    public string GetDeleteStatement(ITypeSchema typeSchema, IDictionary<string, object> keys)
    {
        var keyProps = typeSchema.KeyProperties;
        ValidateKeyParameters(keyProps, keys);

        var sql = $"{SqlBuilder.Keywords.Delete} " +
                  $"{SqlBuilder.Keywords.From} " +
                  $"{SqlBuilder.RenderTableName(typeSchema)} " +
                  $"{SqlBuilder.Keywords.Where} " +
                  $"{SqlBuilder
                      .RenderJoinExpressions(keys
                          .Select(kp => $"{SqlBuilder
                              .RenderPropertyParameterComparison(keyProps.Single(p => p.Name.ColumnName == kp.Key), SqlBuilder.Keywords.CompareEqualsOperator)}"), $" {SqlBuilder.Keywords.And}")}";

        OnTypeSqlProvided(sql, typeSchema);
        return sql;
    }

    /// <summary> Executes the 'type SQL provided' action.</summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="sql">      The SQL. </param>
    /// <param name="schema">   The schema. </param>
    /// <param name="provider"> (Optional) The provider. </param>
    protected virtual void OnTypeSqlProvided(string sql, ITypeSchema schema, [CallerMemberName] string? provider = null)
    {
        if (provider == null)
            throw new ArgumentNullException(nameof(provider));

        SqlProvided?.Invoke(this, new TypeSqlProvidedEventArgs(sql, provider, schema));
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
            _ = keyPropertySchemata.SingleOrDefault(kp => kp.Name.ColumnName == k.Key) ??
                throw new KeyParameterMismatchException(keyPropertySchemata, parameterKeys);
    }
}