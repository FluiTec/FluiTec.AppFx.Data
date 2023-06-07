using System;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   Attribute for key. </summary>
[AttributeUsage(AttributeTargets.Property)]
public class EntityKeyAttribute : Attribute
{
    /// <summary>   Default constructor. </summary>
    public EntityKeyAttribute() : this(0)
    {
    }

    /// <summary>   Constructor. </summary>
    /// <param name="order">    The order. </param>
    public EntityKeyAttribute(int order)
    {
        Order = order;
    }

    /// <summary>
    ///     Gets the order.
    /// </summary>
    /// <value>
    ///     The order.
    /// </value>
    public int Order { get; }
}