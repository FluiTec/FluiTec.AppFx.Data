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

    /// <summary>   Gets insert single statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert single statement. </returns>
    string GetInsertSingleStatement(ITypeSchema typeSchema);

    /// <summary>   Gets retrieve automatic insert key statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The retrieve automatic insert key statement. </returns>
    string GetRetrieveAutoInsertKeyStatement(ITypeSchema typeSchema);

    /// <summary>   Gets insert single automatic statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert single automatic statement. </returns>
    string GetInsertSingleAutoStatement(ITypeSchema typeSchema);

    /// <summary>   Gets insert multiple statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert multiple statement. </returns>
    string GetInsertMultipleStatement(ITypeSchema typeSchema);

    /// <summary>   Gets insert multiple automatic statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The insert multiple automatic statement. </returns>
    string GetInsertMultipleAutoStatement(ITypeSchema typeSchema);

    /// <summary>   Gets update statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The update statement. </returns>
    string GetUpdateStatement(ITypeSchema typeSchema);

    /// <summary>   Gets delete statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <returns>   The delete statement. </returns>
    string GetDeleteStatement(ITypeSchema typeSchema);

    /// <summary>   Gets delete statement. </summary>
    /// <param name="typeSchema">   The type schema. </param>
    /// <param name="keys">         The keys. </param>
    /// <returns>   The delete statement. </returns>
    string GetDeleteStatement(ITypeSchema typeSchema, IDictionary<string, object> keys);
}