using System;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using FluiTec.AppFx.Data.Migration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.Dapper.Tests.Migration
{
    [TestClass]
    public class DapperDataMigratorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThrowsOnMissingScanAssemblies()
        {
            var versionMock = new Mock<IVersionTableMetaData>();
            var unused = new DataMigrator(string.Empty, null, versionMock.Object, builder => { });
        }

        [TestMethod]
        public void TestWontThrowOnMissingMigrations()
        {
            var versionMock = new Mock<IVersionTableMetaData>();
            var unused = new DataMigrator(string.Empty, Array.Empty<Assembly>(), versionMock.Object,
                builder => { builder.AddSQLite(); });
        }
    }
}