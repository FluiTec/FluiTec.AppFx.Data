using System;

namespace FluiTec.AppFx.Data.EntityNames;

/// <summary>   Interface for entity name service. </summary>
public interface IEntityNameService
{
    /// <summary>   Gets a name. </summary>
    /// <param name="type"> The type. </param>
    /// <returns>   The name. </returns>
    EntityName GetName(Type type);
}