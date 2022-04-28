using System;

namespace FluiTec.AppFx.Data.Sql.Attributes;

/// <summary>
///     Attribute for SQL key.
/// </summary>
public class SqlKeyAttribute : Attribute
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public SqlKeyAttribute() : this(true, 0)
    {
    }

    /// <summary>
    ///     Constructor.
    /// </summary>
    /// <param name="identityKey">  True if identity key, false if not. </param>
    /// <param name="order">        The order. </param>
    public SqlKeyAttribute(bool identityKey, int order)
    {
        IdentityKey = identityKey;
        Order = order;
    }

    /// <summary>
    ///     Gets a value indicating whether the identity key.
    /// </summary>
    /// <value>
    ///     True if identity key, false if not.
    /// </value>
    public bool IdentityKey { get; }

    /// <summary>
    ///     Gets the order.
    /// </summary>
    /// <value>
    ///     The order.
    /// </value>
    public int Order { get; }
}