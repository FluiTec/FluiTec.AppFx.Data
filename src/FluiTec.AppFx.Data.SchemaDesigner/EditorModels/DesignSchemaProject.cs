namespace FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

/// <summary>   A design schema project. </summary>
public class DesignSchemaProject : CollectionSaveChangesAware
{
    /// <summary>   The schemata. </summary>
    private ObservableCollectionWithItemNotify<DesignSchema> _schemata = null!;

    /// <summary>   Gets or sets the schemata. </summary>
    /// <value> The schemata. </value>
    public ObservableCollectionWithItemNotify<DesignSchema> Schemata
    {
        get => _schemata;
        set => SetCollection(ref _schemata!, value);
    }
}