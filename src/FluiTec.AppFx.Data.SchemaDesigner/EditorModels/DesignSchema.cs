namespace FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

/// <summary>   A design schema. </summary>
public class DesignSchema : CollectionSaveChangesAware
{
    /// <summary>   The entities. </summary>
    private ObservableCollectionWithItemNotify<DesignEntity> _entities = null!;

    /// <summary>   The name. </summary>
    private string _name = null!;
    
    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    /// <summary>   Gets or sets the entities. </summary>
    /// <value> The entities. </value>
    public ObservableCollectionWithItemNotify<DesignEntity> Entities
    {
        get => _entities;
        set => SetCollection(ref _entities!, value);
    }
}