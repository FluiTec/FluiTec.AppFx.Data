namespace FluiTec.AppFx.Data.SchemaDesigner.DataModels;

/// <summary>   A data schema project. </summary>
public class DataSchemaProject
{
    /// <summary>   Gets or sets the schemata. </summary>
    /// <value> The schemata. </value>
    public List<DataSchema> Schemata { get; set; } = new();
}