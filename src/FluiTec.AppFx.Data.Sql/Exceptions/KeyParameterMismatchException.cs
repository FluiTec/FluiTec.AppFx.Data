using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.Exceptions;

/// <summary>   Exception for signalling key parameter mismatch errors. </summary>
public class KeyParameterMismatchException : Exception
{
    /// <summary>   Gets the entity keys. </summary>
    /// <value> The entity keys. </value>
    public IEnumerable<IKeyPropertySchema> EntityKeys { get; }
    /// <summary>   Gets the parameter keys. </summary>
    /// <value> The parameter keys. </value>
    public IDictionary<string, object> ParameterKeys { get; }

    /// <summary>   Constructor. </summary>
    /// <param name="entityKeys">       The entity keys. </param>
    /// <param name="parameterKeys">    The parameter keys. </param>
    public KeyParameterMismatchException(IEnumerable<IKeyPropertySchema> entityKeys,
        IDictionary<string, object> parameterKeys) : base("KeyParameters don't match available EntityKeys")
    {
        EntityKeys = entityKeys;
        ParameterKeys = parameterKeys;
    }
}