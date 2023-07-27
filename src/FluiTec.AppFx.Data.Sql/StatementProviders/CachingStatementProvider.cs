using System;
using System.Collections.Concurrent;
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

    /// <summary> Gets or add statement.</summary>
    /// <param name="typeSchema">    The type schema. </param>
    /// <param name="statementName"> (Optional) Name of the statement. </param>
    /// <returns> The or add statement.</returns>
    protected string GetOrAddStatement(ITypeSchema typeSchema, [CallerMemberName] string? statementName = null)
    {
        var key = GetKey(typeSchema, statementName);
        if (_statementCache.TryGetValue(key, out var statement))
        {
            OnTypeSqlProvided(statement, typeSchema, statementName);
            return statement;
        }

        var value = SourceStatementProvider.GetAllStatement(typeSchema);
        _statementCache[key] = value;
        return value;
    }
    
    /// <summary> Gets all statement.</summary>
    /// <param name="typeSchema"> The type schema. </param>
    /// <returns> all statement.</returns>
    public string GetAllStatement(ITypeSchema typeSchema)
    {
        return GetOrAddStatement(typeSchema);
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
}