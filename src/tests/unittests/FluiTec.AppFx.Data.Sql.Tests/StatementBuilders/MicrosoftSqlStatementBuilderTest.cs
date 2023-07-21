using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.StatementBuilders;
using FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests.StatementBuilders;

[TestClass]
public class MicrosoftSqlStatementBuilderTest
{
    private static IStatementBuilder GetBuilder()
    {
        return new MicrosoftSqlStatementBuilder();
    }

    private static ITypeSchema GetSchema(Type type)
    {
        return new TypeSchema(type, new AttributeEntityNameService(), new AttributePropertyNameService());
    }

    [TestMethod]
    public void CanGetSqlBuilder()
    {
        var builder = GetBuilder();
        Assert.IsNotNull(builder.SqlBuilder);
    }

    [TestMethod]
    public void CanCreateGetAllStatement()
    {
        var builder = GetBuilder();

        Assert.AreEqual(
            "SELECT [Id] FROM [DummyEntityWithProperty]",
            builder.GetAllStatement(GetSchema(typeof(DummyEntityWithProperty)))
        );

        Assert.AreEqual(
            "SELECT [ID] FROM [DummyEntityWithDecoratedProperty]",
            builder.GetAllStatement(GetSchema(typeof(DummyEntityWithDecoratedProperty)))
        );

        Assert.AreEqual(
            "SELECT [ID] FROM [Test].[Dummy]",
            builder.GetAllStatement(GetSchema(typeof(DecoratedDummyEntityWithDecoratedProperty)))
        );

        Assert.AreEqual(
            "SELECT [Id] FROM [Test].[Dummy]",
            builder.GetAllStatement(GetSchema(typeof(DecoratedDummyEntityWithProperty)))
        );
    }
}