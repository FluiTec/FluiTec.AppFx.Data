using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;

[EntityName("Test", "Dummy")]
public class MultiKeyMultiValueDummy
{
    [EntityKey(0)] public int Id1 { get; set; }

    [EntityKey(1)] public int Id2 { get; set; }

    public string Name1 { get; set; }

    public string Name2 { get; set; }
}