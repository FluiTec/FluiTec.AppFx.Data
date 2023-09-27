namespace FluiTec.AppFx.Data.SchemaDesigner.EditorModels;

/// <summary>   A design property. </summary>
public class DesignProperty : SaveChangesAware
{
    private int? _keyOrder;
    private string _name = null!;
    private bool _nullable;
    private bool _identityKey;
    private Type _type = null!;

    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }

    /// <summary>   Gets or sets the name of the property. </summary>
    /// <value> The name of the property. </value>
    public Type Type
    {
        get => _type;
        set => SetField(ref _type, value);
    }

    /// <summary>   Gets or sets a value indicating whether this object is nullable. </summary>
    /// <value> True if nullable, false if not. </value>
    public bool Nullable
    {
        get => _nullable;
        set => SetField(ref _nullable, value);
    }

    /// <summary>   Gets or sets a value indicating whether the identity key. </summary>
    /// <value> True if identity key, false if not. </value>
    public bool IdentityKey
    {
        get => _identityKey;
        set => SetField(ref _identityKey, value);
    }

    /// <summary>   Gets or sets the key order. </summary>
    /// <value> The key order. </value>
    public int? KeyOrder
    {
        get => _keyOrder;
        set => SetField(ref _keyOrder, value);
    }
}