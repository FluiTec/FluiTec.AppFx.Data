namespace FluiTec.AppFx.Data.SchemaDesigner.DataModels;

/// <summary>   A data schema. </summary>
public class DataSchema
{
    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public string Name { get; set; } = null!;

    /// <summary>   Gets or sets the entities. </summary>
    /// <value> The entities. </value>
    public List<DataEntity> Entities { get; set; } = new();
}