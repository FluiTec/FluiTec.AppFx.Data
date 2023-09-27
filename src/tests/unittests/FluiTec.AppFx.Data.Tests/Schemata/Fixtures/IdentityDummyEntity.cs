using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Tests.Schemata.Fixtures;

public class IdentityDummyEntity
{
    [IdentityKey]
    [EntityKey]
    public int Id { get; set; }
}