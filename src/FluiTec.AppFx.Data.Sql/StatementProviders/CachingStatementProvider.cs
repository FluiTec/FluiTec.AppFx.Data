using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.StatementProviders.EventArguments;

namespace FluiTec.AppFx.Data.Sql.StatementProviders;

/// <summary> A caching statement provider.</summary>
public class CachingStatementProvider : IStatementProvider
{
    public event EventHandler<SqlProvidedEventArgs>? SqlProvided;

    /// <summary> The statement cache.</summary>
    private readonly ConcurrentDictionary<string, string> _statementCache = new();

    /// <summary> Constructor.</summary>
    /// <param name="sourceStatementProvider"> Source statement provider. </param>
    public CachingStatementProvider(IStatementProvider sourceStatementProvider)
    {
        SourceStatementProvider = sourceStatementProvider;
        SourceStatementProvider.SqlProvided += (sender, args) => SqlProvided?.Invoke(sender, args);
    }

    /// <summary> Gets source statement provider.</summary>
    /// <value> The source statement provider.</value>
    public IStatementProvider SourceStatementProvider { get; }

    /// <summary> Gets a key.</summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="typeSchema">    The type schema. </param>
    /// <param name="statementName"> (Optional) Name of the statement. </param>
    /// <returns> The key.</returns>
    protected string GetKey(ITypeSchema typeSchema, [CallerMemberName] string? statementName = null)
    {
        if (statementName == null)
            throw new ArgumentNullException(nameof(statementName));

        return $"{typeSchema.Type.FullName}:{statementName}";
    }

    /// <summary> Gets a key.</summary>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="typeSchema">     The type schema. </param>
    /// <param name="parameterNames"> List of names of the parameters. </param>
    /// <param name="statementName">  (Optional) Name of the statement. </param>
    /// <returns> The key.</returns>
    protected string GetKey(ITypeSchema typeSchema, IEnumerable<string> parameterNames,
        [CallerMemberName] string? statementName = null)
    {
        if (statementName == null)
            throw new ArgumentNullException(nameof(statementName));

        return $"{typeSchema.Type.FullName}:{statementName}:{string.Join('-', parameterNames)}";
    }

    /// <summary> Gets or add statement.</summary>
    /// <param name="key">           The key. </param>
    /// <param name="typeSchema">    The type schema. </param>
    /// <param name="statementName"> (Optional) Name of the statement. </param>
    /// <returns> The or add statement.</returns>
    protected string GetOrAddStatement(string key, ITypeSchema typeSchema, [CallerMemberName] string? statementName = null)
    {
        if (_statementCache.TryGetValue(key, out var statement))
        {
            OnTypeSqlProvided(statement, typeSchema, statementName);
            return statement;
        }

        var value = SourceStatementProvider.GetAllStatement(typeSchema);
        _statementCache[key] = value;
        return value;
    }

    /// <summary> Gets or add statement.</summary>
    /// <param name="typeSchema">    The type schema. </param>
    /// <param name="statementName"> (Optional) Name of the statement. </param>
    /// <returns> The or add statement.</returns>
    protected string GetOrAddStatement(ITypeSchema typeSchema, [CallerMemberName] string? statementName = null)
    {
        var key = GetKey(typeSchema, statementName);
        return GetOrAddStatement(key, typeSchema, statementName);
    }

    /// <summary> Gets or add statement.</summary>
    /// <param name="typeSchema">     The type schema. </param>
    /// <param name="parameterNames"> List of names of the parameters. </param>
    /// <param name="statementName">  (Optional) Name of the statement. </param>
    /// <returns> The or add statement.</returns>
    protected string GetOrAddStatement(ITypeSchema typeSchema, IEnumerable<string> parameterNames,
        [CallerMemberName] string? statementName = null)
    {
        var key = GetKey(typeSchema, parameterNames, statementName);
        return GetOrAddStatement(key, typeSchema, statementName);
    }

    /// <summary>   Executes the 'type SQL provided' action. </summary>
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

    /// <summary> Gets all statement.</summary>
    /// <param name="typeSchema"> The type schema. </param>
    /// <returns> all statement.</returns>
    public string GetAllStatement(ITypeSchema typeSchema)
    {
        return GetOrAddStatement(typeSchema);
    }

    /// <summary> Gets count statement.</summary>
    /// <param name="typeSchema"> The type schema. </param>
    /// <returns> The count statement.</returns>
    public string GetCountStatement(ITypeSchema typeSchema)
    {
        return GetOrAddStatement(typeSchema);
    }

    /// <summary> Gets paging statement.</summary>
    /// <param name="typeSchema">        The type schema. </param>
    /// <param name="skipParameterName"> Name of the skip parameter. </param>
    /// <param name="takeParameterName"> Name of the take parameter. </param>
    /// <returns> The paging statement.</returns>
    public string GetPagingStatement(ITypeSchema typeSchema, string skipParameterName, string takeParameterName)
    {
        return GetOrAddStatement(typeSchema, new[] { skipParameterName, takeParameterName });
    }

    /// <summary>   Gets select by key statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <param name="key">          The key. </param>
    /// <returns>   The select by key statement. </returns>
    public string GetSelectByKeyStatement(ITypeSchema typeSchema, IDictionary<string, object> key)
    {
        return GetOrAddStatement(typeSchema, key.Select(k => k.Key));
    }

    /// <summary>   Gets insert single statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert single statement. </returns>
    public string GetInsertSingleStatement(ITypeSchema typeSchema)
    {
        return GetOrAddStatement(typeSchema);
    }

    /// <summary>   Gets retrieve automatic insert key statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The retrieve automatic insert key statement. </returns>
    public string GetRetrieveAutoInsertKeyStatement(ITypeSchema typeSchema)
    {
        return GetOrAddStatement(typeSchema);
    }

    /// <summary>   Gets insert single automatic statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert single automatic statement. </returns>
    public string GetInsertSingleAutoStatement(ITypeSchema typeSchema)
    {
        return GetOrAddStatement(typeSchema);
    }

    /// <summary>   Gets insert multiple statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert multiple statement. </returns>
    public string GetInsertMultipleStatement(ITypeSchema typeSchema)
    {
        return GetOrAddStatement(typeSchema);
    }

    /// <summary>   Gets insert multiple automatic statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert multiple automatic statement. </returns>
    public string GetInsertMultipleAutoStatement(ITypeSchema typeSchema)
    {
        return GetOrAddStatement(typeSchema);
    }

    /// <summary>   Gets update statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The update statement. </returns>
    public string GetUpdateStatement(ITypeSchema typeSchema)
    {
        return GetOrAddStatement(typeSchema);
    }
}