using System;

namespace FluiTec.AppFx.Data.Reflection;

/// <summary>   Exception for signalling non singular identity key errors. </summary>
public class NonSingularIdentityKeyException : Exception
{
    /// <summary>   Gets the type. </summary>
    /// <value> The type. </value>
    public Type Type { get; }

    /// <summary>   Constructor. </summary>
    /// <param name="type"> The type. </param>
    public NonSingularIdentityKeyException(Type type)
    {
        Type = type;
    }
}