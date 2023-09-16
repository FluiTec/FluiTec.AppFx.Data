using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;

[EntityName("Test", "Dummy")]
public class DecoratedDummyEntityWithDecoratedProperty
{
    [EntityKey]
    [PropertyName("ID")] 
    public int Id { get; set; }
}