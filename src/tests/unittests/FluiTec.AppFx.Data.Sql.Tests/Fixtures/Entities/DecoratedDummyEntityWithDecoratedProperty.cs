using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;

namespace FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;

[EntityName("Test", "Dummy")]
public class DecoratedDummyEntityWithDecoratedProperty
{
    [PropertyName("ID")] public int Id { get; set; }
}