using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.StatementProviders.EventArguments;

namespace FluiTec.AppFx.Data.Sql.StatementProviders;

/// <summary> Interface for statement provider.</summary>
public interface IStatementProvider
{
    /// <summary> Event queue for all listeners interested in SqlProvided events.</summary>
    event EventHandler<SqlProvidedEventArgs>? SqlProvided;
    
    /// <summary>   Gets all statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   all statement. </returns>
    string GetAllStatement(ITypeSchema typeSchema);

    /// <summary> Gets count statement.</summary>
    /// <param name="typeSchema"> The type schema. </param>
    /// <returns> The count statement.</returns>
    string GetCountStatement(ITypeSchema typeSchema);

    /// <summary> Gets paging statement.</summary>
    /// <param name="typeSchema">        The type schema. </param>
    /// <param name="skipParameterName"> Name of the skip parameter. </param>
    /// <param name="takeParameterName"> Name of the take parameter. </param>
    /// <returns> The paging statement.</returns>
    string GetPagingStatement(ITypeSchema typeSchema, string skipParameterName, string takeParameterName);

    /// <summary>   Gets select by key statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <param name="keys">          The keys. </param>
    /// <returns>   The select by key statement. </returns>
    string GetSelectByKeyStatement(ITypeSchema typeSchema, IDictionary<string, object> keys);
}