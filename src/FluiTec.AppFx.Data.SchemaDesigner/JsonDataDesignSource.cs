using FluiTec.AppFx.Data.SchemaDesigner.DataModels;
using Newtonsoft.Json;

namespace FluiTec.AppFx.Data.SchemaDesigner;

/// <summary>   A JSON data design source. </summary>
public class JsonDataDesignSource : ModelDataDesignSource
{
    /// <summary>   Constructor. </summary>
    /// <param name="fileName"> The name of the file. </param>
    public JsonDataDesignSource(string fileName)
    {
        FileName = fileName;
    }

    /// <summary>   Gets the filename of the file. </summary>
    /// <value> The name of the file. </value>
    public string FileName { get; }

    /// <summary>   Loads the project. </summary>
    /// <returns>   The project. </returns>
    protected override DataSchemaProject LoadProject()
    {
        if (!File.Exists(FileName)) return new DataSchemaProject();

        using var file = File.OpenText(FileName);
        using var reader = new JsonTextReader(file);
        var serializer = new JsonSerializer();
        return serializer.Deserialize<DataSchemaProject>(reader)!;
    }

    /// <summary>   Saves the given design. </summary>
    /// <param name="project">  The project to save. </param>
    protected override void Save(DataSchemaProject project)
    {
        using var file = File.CreateText(FileName);
        var serializer = new JsonSerializer
        {
            Formatting = Formatting.Indented
        };
        serializer.Serialize(file, project);
    }
}