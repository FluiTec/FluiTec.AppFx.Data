using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services;

/// <summary>   A service for accessing entities information. </summary>
public class EntityService : PropertyChangedBase, IEntityService
{
    /// <summary>   The current entity. </summary>
    private DesignEntity? _currentEntity;

    /// <summary>   Gets or sets the current entity. </summary>
    /// <value> The current entity. </value>
    public DesignEntity? CurrentEntity
    {
        get => _currentEntity;
        set => SetField(ref _currentEntity, value);
    }
}