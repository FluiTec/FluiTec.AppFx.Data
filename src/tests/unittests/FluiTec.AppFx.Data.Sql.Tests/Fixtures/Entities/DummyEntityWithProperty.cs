using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;

public class DummyEntityWithProperty
{
    [EntityKey] public int Id { get; set; }
}