using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services;

/// <summary>   A service for accessing schemas information. </summary>
public class SchemaService : PropertyChangedBase, ISchemaService
{
    /// <summary>   The current schema. </summary>
    private DesignSchema? _currentSchema;

    /// <summary>   Gets or sets the current schema. </summary>
    /// <value> The current schema. </value>
    public DesignSchema? CurrentSchema
    {
        get => _currentSchema;
        set => SetField(ref _currentSchema, value);
    }
}