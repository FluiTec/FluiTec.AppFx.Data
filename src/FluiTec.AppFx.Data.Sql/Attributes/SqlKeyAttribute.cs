using System;

namespace FluiTec.AppFx.Data.Sql.Attributes;

/// <summary>
/// Attribute for SQL key.
/// </summary>
public class SqlKeyAttribute : Attribute
{
    /// <summary>
    /// Gets a value indicating whether the identity key.
    /// </summary>
    ///
    /// <value>
    /// True if identity key, false if not.
    /// </value>
    public bool IdentityKey { get; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public SqlKeyAttribute() : this(true)
    {

    }

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="identityKey">  True if identity key, false if not. </param>
    public SqlKeyAttribute(bool identityKey)
    {
        IdentityKey = identityKey;
    }
}