namespace FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

/// <summary>   A design entity. </summary>
public class DesignEntity : CollectionSaveChangesAware
{
    /// <summary>   The name. </summary>
    private string _name = null!;

    /// <summary>   The properties. </summary>
    private ObservableCollectionWithItemNotify<DesignProperty> _properties = null!;

    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    /// <summary>   Gets or sets the entities. </summary>
    /// <value> The entities. </value>
    public ObservableCollectionWithItemNotify<DesignProperty> Properties
    {
        get => _properties;
        set => SetCollection(ref _properties, value);
    }
}