using System;

namespace FluiTec.AppFx.Data.Schemata;

/// <summary>   Exception for signalling schema errors. </summary>
public class SchemaException : Exception
{
    /// <summary>   Gets the schema. </summary>
    /// <value> The schema. </value>
    public ISchema Schema { get; }

    /// <summary>   Constructor. </summary>
    /// <param name="schema">   The schema. </param>
    public SchemaException(ISchema schema)
    {
        Schema = schema;
    }
}