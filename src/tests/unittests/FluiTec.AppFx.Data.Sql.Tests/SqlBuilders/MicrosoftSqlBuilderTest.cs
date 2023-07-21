﻿using FluiTec.AppFx.Data.EntityNames;
using FluiTec.AppFx.Data.PropertyNames;
using FluiTec.AppFx.Data.Reflection;
using FluiTec.AppFx.Data.Sql.Enums;
using FluiTec.AppFx.Data.Sql.SqlBuilders;
using FluiTec.AppFx.Data.Sql.Tests.Fixtures.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Sql.Tests.SqlBuilders;

/// <summary>   (Unit Test Class) a microsoft SQL builder test. </summary>
[TestClass]
public class MicrosoftSqlBuilderTest
{
    private static ISqlBuilder GetBuilder() => new MicrosoftSqlBuilder();

    private static ITypeSchema GetSchema(Type type) =>
        new TypeSchema(type, new AttributeEntityNameService(), new AttributePropertyNameService());

    [TestMethod]
    public void CanGetType()
    {
        var builder = GetBuilder();
        Assert.AreEqual(SqlType.Mssql, builder.SqlType);
    }

    [TestMethod]
    public void HasKeywords()
    {
        var builder = GetBuilder();
        Assert.IsNotNull(builder.Keywords);
    }

    [TestMethod]
    public void SupportsSchemata()
    {
        var builder = GetBuilder();
        Assert.IsTrue(builder.SupportsSchemata);
    }

    [TestMethod]
    public void CanRenderTableName()
    {
        var builder = GetBuilder();

        Assert.AreEqual(
            $"[{nameof(EmptyUndecoratedDummyEntity)}]", 
            builder.RenderTableName(GetSchema(typeof(EmptyUndecoratedDummyEntity)))
        );

        Assert.AreEqual(
            "[Dummy]",
            builder.RenderTableName(GetSchema(typeof(EmptyDecoratedDummyEntity)))
        );

        Assert.AreEqual(
            "[Test].[Dummy]",
            builder.RenderTableName(GetSchema(typeof(EmptyDecoratedWithSchemaDummyEntity)))
        );
    }

    [TestMethod]
    public void CanRenderProperty()
    {
        var builder = GetBuilder();

        Assert.AreEqual(
            "[Id]",
            builder.RenderProperty(GetSchema(typeof(DummyEntityWithProperty)).Properties.Single())
            );

        Assert.AreEqual(
            "[ID]",
            builder.RenderProperty(GetSchema(typeof(DummyEntityWithDecoratedProperty)).Properties.Single())
        );
    }

    [TestMethod]
    public void CanRenderList()
    {
        var builder = GetBuilder();

        Assert.AreEqual("[Id], [Name]", builder.RenderList(new []{"[Id]", "[Name]"}));
    }
}