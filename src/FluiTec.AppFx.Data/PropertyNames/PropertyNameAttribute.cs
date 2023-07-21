using System;

namespace FluiTec.AppFx.Data.PropertyNames;

/// <summary>   Attribute for Property name. </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PropertyNameAttribute : Attribute
{
    /// <summary>   Constructor. </summary>
    public PropertyNameAttribute(string columnName)
    {
        ColumnName = columnName;
    }

    /// <summary>   Gets the name of the column. </summary>
    /// <value> The name of the column. </value>
    public string ColumnName { get; }
}