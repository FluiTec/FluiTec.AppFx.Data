using System;

namespace FluiTec.AppFx.Data.EntityNameServices;

/// <summary>	Attribute for entity name. </summary>
[AttributeUsage(AttributeTargets.Class)]
public class EntityNameAttribute : Attribute
{
    #region Properties

    /// <summary>
    /// Gets the schema.
    /// </summary>
    ///
    /// <value>
    /// The schema.
    /// </value>
    public string Schema { get; }

    /// <summary>
    /// Gets the name only.
    /// </summary>
    ///
    /// <value>
    /// The name only.
    /// </value>
    public string NameOnly { get; }

    /// <summary>	Gets or sets the name. </summary>
    /// <value>	The name of the entity. </value>
    public string Name { get; set; }

    #endregion

    #region Constructors

    /// <summary>	Constructor. </summary>
    /// <param name="name">	The name of the entity. </param>
    public EntityNameAttribute(string name)
    {
        Name = name;
        NameOnly = name;
    }

    /// <summary>   Constructor. </summary>
    /// <param name="schema">   The schema. </param>
    /// <param name="name">     The name of the entity. </param>
    public EntityNameAttribute(string schema, string name)
    {
        Schema = schema;
        NameOnly = name;
        Name = $"{schema}.{name}";
    }

    #endregion
}