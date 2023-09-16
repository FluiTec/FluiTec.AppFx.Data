using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;

public class DummyEntityWithDecoratedProperty
{
    [EntityKey]
    [PropertyName("ID")]
    public int Id { get; set; }
}