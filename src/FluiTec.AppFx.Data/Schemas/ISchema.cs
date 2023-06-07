using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Schemas;

/// <summary>   Interface for schema. </summary>
public interface ISchema : IEnumerable<ITypeSchema>
{
    /// <summary>   Indexer to get items within this collection using array index syntax. </summary>
    /// <param name="entityType">   Type of the entity. </param>
    /// <returns>   The indexed item. </returns>
    ITypeSchema this[Type entityType] { get; }
}