using System;

namespace FluiTec.AppFx.Data.EntityNameServices;

/// <summary>EntityNameService using the classes name.</summary>
/// <seealso cref="FluiTec.AppFx.Data.EntityNameServices.IEntityNameService" />
public class ClassEntityNameService : IEntityNameService
{
    /// <summary>Names the specified type.</summary>
    /// <param name="type">The type.</param>
    /// <returns>A name for the entityType.</returns>
    /// <exception cref="System.ArgumentNullException">type</exception>
    public virtual string Name(Type type)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));
        return type.Name;
    }

    /// <summary>
    ///     Schema and name.
    /// </summary>
    /// <param name="type"> The type. </param>
    /// <returns>
    ///     A Tuple&lt;string,string&gt;
    /// </returns>
    public virtual Tuple<string, string> SchemaAndName(Type type)
    {
        return new Tuple<string, string>(null, Name(type));
    }
}