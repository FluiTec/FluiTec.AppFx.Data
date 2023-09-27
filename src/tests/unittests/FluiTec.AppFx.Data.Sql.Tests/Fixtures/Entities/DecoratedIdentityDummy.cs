using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.Reflection;

namespace FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;

[EntityName("Test", "Dummy")]
public class DecoratedIdentityDummy
{
    [EntityKey] [IdentityKey] public int Id { get; set; }

    public string Name { get; set; }
}