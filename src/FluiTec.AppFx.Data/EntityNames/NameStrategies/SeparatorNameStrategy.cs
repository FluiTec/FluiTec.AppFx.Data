using System;

namespace FluiTec.AppFx.Data.EntityNames.NameStrategies;

/// <summary>   A separator strategy. </summary>
public class SeparatorNameStrategy : INameStrategy
{
    /// <summary>   Constructor. </summary>
    /// <param name="separator">    The separator. </param>
    public SeparatorNameStrategy(string separator)
    {
        Separator = separator;
    }

    /// <summary>   Gets the separator. </summary>
    /// <value> The separator. </value>
    public string Separator { get; }

    /// <summary>   Convert this object into a string representation. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="type">         The type. </param>
    /// <param name="nameService">  The name service. </param>
    /// <returns>   A string that represents this object. </returns>
    public string ToString(Type type, IEntityNameService nameService)
    {
        if (type == null)
            throw new ArgumentNullException(nameof(type));

        if (nameService == null)
            throw new ArgumentNullException(nameof(nameService));

        return ToString(nameService.GetName(type));
    }

    /// <summary>   Convert this object into a string representation. </summary>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when one or more required arguments are
    ///     null.
    /// </exception>
    /// <param name="entityName">   Name of the entity. </param>
    /// <returns>   A string that represents this object. </returns>
    public virtual string ToString(EntityName entityName)
    {
        if (entityName == null)
            throw new ArgumentNullException(nameof(entityName));

        return entityName.Schema != null
            ? $"{entityName.Schema}{Separator}{entityName.Name}"
            : entityName.Name;
    }
}