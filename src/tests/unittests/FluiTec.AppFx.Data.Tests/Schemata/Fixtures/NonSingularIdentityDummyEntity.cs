using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Tests.Schemata.Fixtures;

public class NonSingularIdentityDummyEntity
{
    [IdentityKey] [EntityKey(0)] public int Id1 { get; set; }

    [IdentityKey] [EntityKey(2)] public int Id2 { get; set; }
}