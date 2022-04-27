using System;

namespace FluiTec.AppFx.Data.EntityNameServices;

/// <summary>	Interface for entity name service. </summary>
public interface IEntityNameService
{
    /// <summary>Names the specified type.</summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    string Name(Type type);

    /// <summary>
    /// Schema and name.
    /// </summary>
    ///
    /// <param name="type"> The type. </param>
    ///
    /// <returns>
    /// A Tuple&lt;string,string&gt;
    /// </returns>
    Tuple<string, string> SchemaAndName(Type type);
}