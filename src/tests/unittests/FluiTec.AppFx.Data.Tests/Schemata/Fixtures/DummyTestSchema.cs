using FluiTec.AppFx.Data.Schemata;

namespace FluiTec.AppFx.Data.Tests.Schemata.Fixtures;

public class DummyTestSchema : Schema
{
    public DummyTestSchema()
    {
        AddEntity(typeof(DummyEntity));
    }
}