namespace FluiTec.AppFx.Data.SchemaDesigner.DataModels;

/// <summary>   A data entity. </summary>
public class DataEntity
{
    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public string Name { get; set; } = null!;

    /// <summary>   Gets or sets the properties. </summary>
    /// <value> The properties. </value>
    public List<DataProperty> Properties { get; set; } = new();
}