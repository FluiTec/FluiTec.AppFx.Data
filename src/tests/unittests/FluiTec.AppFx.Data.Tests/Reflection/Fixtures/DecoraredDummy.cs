using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Tests.Reflection.Fixtures;

public class DecoraredDummy
{
    [EntityKey] public int Id1 { get; set; }

    [EntityKey(1)] public int Id2 { get; set; }

    public string? Name { get; set; }

    [Unmapped] public string? Unmapped { get; set; }
}