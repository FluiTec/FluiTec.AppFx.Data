using FluiTec.AppFx.Data.SchemaDesigner.EditorModels;
using FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services.Interfaces;

namespace FluiTec.AppFx.Data.SchemaDesigner.Wpfui.Services;

/// <summary>   A service for accessing properties information. </summary>
public class PropertyService : PropertyChangedBase, IPropertyService
{
    /// <summary>   The current property. </summary>
    private DesignProperty? _currentProperty;

    /// <summary>   Gets or sets the current property. </summary>
    /// <value> The current property. </value>
    public DesignProperty? CurrentProperty
    {
        get => _currentProperty;
        set => SetField(ref _currentProperty, value);
    }
}