using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Schemata;

namespace FluiTec.AppFx.Data.Tests.Schemata.Fixtures;

public class EmptyTestSchema : Schema
{
    public EmptyTestSchema() : base(new AttributeEntityNameService(), new AttributePropertyNameService())
    {
    }
}