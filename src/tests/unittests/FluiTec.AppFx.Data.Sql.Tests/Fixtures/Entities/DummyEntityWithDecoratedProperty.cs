using FluiTec.AppFx.Data.PropertyNames;

namespace FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;

public class DummyEntityWithDecoratedProperty
{
    [PropertyName("ID")]
    public int Id { get; set; }
}