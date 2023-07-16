using FluiTec.AppFx.Data.DataProviders;

namespace FluiTec.AppFx.Data.SchemaDesigner.Generator;

/// <summary>   Information about the project. </summary>
public class ProjectInformation
{
    /// <summary>   Gets or sets the pathname of the directory. </summary>
    /// <value> The pathname of the directory. </value>
    public string Directory { get; set; }

    /// <summary>   Gets or sets the name of the project. </summary>
    /// <value> The name of the project. </value>
    public string ProjectName { get; set; }

    /// <summary>   Gets or sets the project extension. </summary>
    /// <value> The project extension. </value>
    public string ProjectExtension { get; set; } = ".csproj";

    /// <summary>   Gets or sets a list of types of the providers. </summary>
    /// <value> A list of types of the providers. </value>
    public IEnumerable<ProviderType> ProviderTypes { get; set; }


}