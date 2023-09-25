using FluiTec.AppFx.Data.Reflection;

namespace Samples.TestData.Entities;

/// <summary>   A dummy entity. </summary>
public class DummyEntity
{
    /// <summary>   Gets or sets the identifier. </summary>
    /// <value> The identifier. </value>
    [EntityKey]
    [IdentityKey]
    public int Id { get; set; }

    /// <summary>   Gets or sets the name. </summary>
    /// <value> The name. </value>
    public string? Name { get; set; }
}