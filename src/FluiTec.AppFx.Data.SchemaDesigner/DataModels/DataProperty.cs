namespace FluiTec.AppFx.Data.SchemaDesigner.DataModels;

/// <summary>   A data property. </summary>
public class DataProperty
{
    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public string Name { get; set; } = null!;

    public string TypeName { get; set; } = null!;

    /// <summary>   Gets or sets a value indicating whether this object is nullable. </summary>
    /// <value> True if nullable, false if not. </value>
    public bool Nullable { get; set; } = false;

    /// <summary>   Gets or sets a value indicating whether the identity key. </summary>
    /// <value> True if identity key, false if not. </value>
    public bool IdentityKey { get; set; } = false;

    /// <summary>   Gets or sets the key order. </summary>
    /// <value> The key order. </value>
    public int? KeyOrder { get; set; } = null!;
}