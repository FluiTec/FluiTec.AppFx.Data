using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.Exceptions;
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

    [TestMethod]
    public void CanCreateCountStatement()
    {
        var builder = GetBuilder();

        Assert.AreEqual(
            "SELECT COUNT(*) FROM [DummyEntityWithProperty]",
            builder.GetCountStatement(GetSchema(typeof(DummyEntityWithProperty)))
        );

        Assert.AreEqual(
            "SELECT COUNT(*) FROM [DummyEntityWithDecoratedProperty]",
            builder.GetCountStatement(GetSchema(typeof(DummyEntityWithDecoratedProperty)))
        );

        Assert.AreEqual(
            "SELECT COUNT(*) FROM [Test].[Dummy]",
            builder.GetCountStatement(GetSchema(typeof(DecoratedDummyEntityWithDecoratedProperty)))
        );

        Assert.AreEqual(
            "SELECT COUNT(*) FROM [Test].[Dummy]",
            builder.GetCountStatement(GetSchema(typeof(DecoratedDummyEntityWithProperty)))
        );
    }

    [TestMethod]
    public void CanCreatePagingStatement()
    {
        var builder = GetBuilder();

        Assert.AreEqual(
            "SELECT [Id] FROM [DummyEntityWithProperty] ORDER BY [Id] ASC OFFSET @skipRecords ROWS FETCH NEXT @takeRecords ROWS ONLY",
            builder.GetPagingStatement(GetSchema(typeof(DummyEntityWithProperty)), "" +
                "skipRecords", "takeRecords")
        );

        Assert.AreEqual(
            "SELECT [ID] FROM [DummyEntityWithDecoratedProperty] ORDER BY [ID] ASC OFFSET @skipRecords ROWS FETCH NEXT @takeRecords ROWS ONLY",
            builder.GetPagingStatement(GetSchema(typeof(DummyEntityWithDecoratedProperty)), 
                "skipRecords", "takeRecords")
        );

        Assert.AreEqual(
            "SELECT [ID] FROM [Test].[Dummy] ORDER BY [ID] ASC OFFSET @skipRecords ROWS FETCH NEXT @takeRecords ROWS ONLY",
            builder.GetPagingStatement(GetSchema(typeof(DecoratedDummyEntityWithDecoratedProperty)), 
                "skipRecords", "takeRecords")
        );

        Assert.AreEqual(
            "SELECT [Id] FROM [Test].[Dummy] ORDER BY [Id] ASC OFFSET @skipRecords ROWS FETCH NEXT @takeRecords ROWS ONLY",
            builder.GetPagingStatement(GetSchema(typeof(DecoratedDummyEntityWithProperty)), 
                "skipRecords", "takeRecords")
        );
    }

    [TestMethod]
    public void CanCreateSelectByKeyStatementForSingleKeys()
    {
        var builder = GetBuilder();

        Assert.AreEqual(
            "SELECT [Id] FROM [DummyEntityWithProperty] WHERE [Id] = @Id",
            builder.GetSelectByKeyStatement(GetSchema(typeof(DummyEntityWithProperty)), new Dictionary<string, object>(
                new[] {new KeyValuePair<string, object>("Id", 1)}))
        );

        Assert.AreEqual(
            "SELECT [ID] FROM [DummyEntityWithDecoratedProperty] WHERE [ID] = @Id",
            builder.GetSelectByKeyStatement(GetSchema(typeof(DummyEntityWithDecoratedProperty)), new Dictionary<string, object>(
                new[] { new KeyValuePair<string, object>("ID", 1) }))
        );

        Assert.AreEqual(
            "SELECT [ID] FROM [Test].[Dummy] WHERE [ID] = @Id",
            builder.GetSelectByKeyStatement(GetSchema(typeof(DecoratedDummyEntityWithDecoratedProperty)), new Dictionary<string, object>(
                new[] { new KeyValuePair<string, object>("ID", 1) }))
        );

        Assert.AreEqual(
            "SELECT [Id] FROM [Test].[Dummy] WHERE [Id] = @Id",
            builder.GetSelectByKeyStatement(GetSchema(typeof(DecoratedDummyEntityWithProperty)), new Dictionary<string, object>(
                new[] { new KeyValuePair<string, object>("Id", 1) }))
        );
    }

    [TestMethod]
    [ExpectedException(typeof(KeyParameterMismatchException))]
    public void ThrowsOnSelectByKeyStatementWithoutEntityKeys()
    {
        var builder = GetBuilder();

        builder.GetSelectByKeyStatement(GetSchema(typeof(EmptyDecoratedDummyEntity)), new Dictionary<string, object>(
            new[] { new KeyValuePair<string, object>("Id", 1) }));
    }

    [TestMethod]
    [ExpectedException(typeof(KeyParameterMismatchException))]
    public void ThrowsOnSelectByKeyStatementWithMissingParams()
    {
        var builder = GetBuilder();

        builder.GetSelectByKeyStatement(GetSchema(typeof(DummyEntityWithProperty)), new Dictionary<string, object>());
    }

    [TestMethod]
    public void CanCreateInsertNonIdentity()
    {
        var builder = GetBuilder();

        // single
        Assert.AreEqual("INSERT INTO [DummyEntityWithProperty] ([Id]) VALUES (@Id)",
            builder.GetInsertSingleStatement(GetSchema(typeof(DummyEntityWithProperty))));

        Assert.AreEqual("INSERT INTO [DummyEntityWithDecoratedProperty] ([ID]) VALUES (@Id)",
            builder.GetInsertSingleStatement(GetSchema(typeof(DummyEntityWithDecoratedProperty))));

        Assert.AreEqual("INSERT INTO [Test].[Dummy] ([ID]) VALUES (@Id)",
            builder.GetInsertSingleStatement(GetSchema(typeof(DecoratedDummyEntityWithDecoratedProperty))));

        Assert.AreEqual("INSERT INTO [Test].[Dummy] ([Id]) VALUES (@Id)",
            builder.GetInsertSingleStatement(GetSchema(typeof(DecoratedDummyEntityWithProperty))));

        //multiple
        Assert.AreEqual("INSERT INTO [DummyEntityWithProperty] ([Id]) VALUES (@Id)",
            builder.GetInsertMultipleStatement(GetSchema(typeof(DummyEntityWithProperty))));

        Assert.AreEqual("INSERT INTO [DummyEntityWithDecoratedProperty] ([ID]) VALUES (@Id)",
            builder.GetInsertMultipleStatement(GetSchema(typeof(DummyEntityWithDecoratedProperty))));

        Assert.AreEqual("INSERT INTO [Test].[Dummy] ([ID]) VALUES (@Id)",
            builder.GetInsertMultipleStatement(GetSchema(typeof(DecoratedDummyEntityWithDecoratedProperty))));

        Assert.AreEqual("INSERT INTO [Test].[Dummy] ([Id]) VALUES (@Id)",
            builder.GetInsertMultipleStatement(GetSchema(typeof(DecoratedDummyEntityWithProperty))));
    }

    [TestMethod]
    public void CanCreateInsertIdentity()
    {
        var builder = GetBuilder();

        // single
        Assert.AreEqual("INSERT INTO [Test].[Dummy] ([Name]) VALUES (@Name) ; SELECT SCOPE_IDENTITY() [Id]",
            builder.GetInsertSingleAutoStatement(GetSchema(typeof(DecoratedIdentityDummy))));

        // multiple
        Assert.AreEqual("INSERT INTO [Test].[Dummy] ([Name]) VALUES (@Name)",
            builder.GetInsertMultipleAutoStatement(GetSchema(typeof(DecoratedIdentityDummy))));
    }

    [TestMethod]
    public void CanCreateUpdateStatement()
    {
        var builder = GetBuilder();
        
        Assert.AreEqual("UPDATE [Test].[Dummy] SET [Name] = @Name WHERE [Id] = @Id",
            builder.GetUpdateStatement(GetSchema(typeof(DecoratedIdentityDummy))));
        Assert.AreEqual("UPDATE [Test].[Dummy] SET [Name1] = @Name1, [Name2] = @Name2 WHERE [Id1] = @Id1 AND [Id2] = @Id2",
            builder.GetUpdateStatement(GetSchema(typeof(MultiKeyMultiValueDummy))));
    }
}