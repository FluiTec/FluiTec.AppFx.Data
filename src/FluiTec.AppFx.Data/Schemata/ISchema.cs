using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Schemata;

/// <summary>   Interface for schema. </summary>
public interface ISchema : IEnumerable<ITypeSchema>
{
    /// <summary>   Indexer to get items within this collection using array index syntax. </summary>
    /// <param name="entityType">   Type of the entity. </param>
    /// <returns>   The indexed item. </returns>
    ITypeSchema this[Type entityType] { get; }

    /// <summary>   Gets the entity name service. </summary>
    /// <value> The entity name service. </value>
    IEntityNameService EntityNameService { get; }

    /// <summary>   Gets the property name service. </summary>
    /// <value> The property name service. </value>
    IPropertyNameService PropertyNameService { get; }
}