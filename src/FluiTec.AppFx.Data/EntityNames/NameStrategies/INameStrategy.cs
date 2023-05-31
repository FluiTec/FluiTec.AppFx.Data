using System;

namespace FluiTec.AppFx.Data.EntityNames.NameStrategies;

/// <summary>   Interface for name strategy. </summary>
public interface INameStrategy
{
    /// <summary>   Convert this object into a string representation. </summary>
    /// <param name="type">         The type. </param>
    /// <param name="nameService">  The name service. </param>
    /// <returns>   A string that represents this object. </returns>
    string ToString(Type type, IEntityNameService nameService);

    /// <summary>   Convert this object into a string representation. </summary>
    /// <param name="entityName">   Name of the entity. </param>
    /// <returns>   A string that represents this object. </returns>
    string ToString(EntityName entityName);
}