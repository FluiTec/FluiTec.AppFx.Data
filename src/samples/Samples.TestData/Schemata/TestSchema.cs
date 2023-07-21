using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Schemata;

namespace Samples.TestData.Schemata;

/// <summary>   A test schema. </summary>
public class TestSchema : Schema
{
    /// <summary>   Constructor. </summary>
    /// <param name="entityNameService">    The entity name service. </param>
    /// <param name="propertyNameService">  The property name service. </param>
    public TestSchema(IEntityNameService entityNameService, IPropertyNameService propertyNameService) 
        : base(entityNameService, propertyNameService)
    {
    }
}