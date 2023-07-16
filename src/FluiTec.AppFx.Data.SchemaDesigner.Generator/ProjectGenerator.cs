using FluiTec.AppFx.Data.SchemaDesigner.DataModels;
using Newtonsoft.Json;

namespace FluiTec.AppFx.Data.SchemaDesigner.Generator;

/// <summary>   A project generator. </summary>
public class ProjectGenerator
{
    /// <summary>   Constructor. </summary>
    /// <param name="project">  The project. </param>
    public ProjectGenerator(DataSchemaProject project)
    {

    }

    /// <summary>   Creates or update. </summary>
    public void CreateOrUpdate(ProjectInformation information)
    {
        if (!Directory.Exists(information.Directory))
            Directory.CreateDirectory(information.Directory);
    }

    /// <summary>   Creates a new object from the given JSON source. </summary>
    /// <exception cref="FileNotFoundException">    Thrown when the requested file is not present. </exception>
    /// <param name="filePath"> Full pathname of the file. </param>
    /// <returns>   A ProjectGenerator. </returns>
    public static ProjectGenerator FromJsonSource(string filePath)
    {
        if  (!File.Exists(filePath)) { throw new FileNotFoundException(); }

        using var file = File.OpenText(filePath);
        using var reader = new JsonTextReader(file);
        var serializer = new JsonSerializer();
        var prj = serializer.Deserialize<DataSchemaProject>(reader)!;

        return new ProjectGenerator(prj);
    }
}