using System;
using System.Reflection;
using System.Reflection.Metadata;
using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using FluiTec.AppFx.Data.Dapper.Migration;
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
            var migrator = new DapperDataMigrator(string.Empty, null, versionMock.Object, builder => {});
        }

        [TestMethod]
        public void TestWontThrowOnMissingMigrations()
        {
            var versionMock = new Mock<IVersionTableMetaData>();
            var migrator = new DapperDataMigrator(string.Empty, new Assembly[0], versionMock.Object,
                builder => { builder.AddSQLite(); });
        }
    }
}
