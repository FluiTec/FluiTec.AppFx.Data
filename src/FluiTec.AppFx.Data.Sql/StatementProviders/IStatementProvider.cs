using System;
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
}