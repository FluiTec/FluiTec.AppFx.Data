using System;
using FluiTec.AppFx.Data.Dapper.Migration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Tests.Migration
{
    [TestClass]
    public class DapperMigrationAttributeTests
    {
        [TestMethod]
        public void TestSavesAuthor()
        {
            const string author = "sample";
            var attribute = new DapperMigrationAttribute(2020, 10, 10, 10, 10, author);
            Assert.AreEqual(author, attribute.Author);
        }

        [TestMethod]
        public void CanCalculateVersion()
        {
            const int year = 2020;
            const int month = 10;
            const int day = 10;
            const int hour = 10;
            const int minute = 10;
            const string author = "sample";

            var version = new DateTime(year, month, day, hour, minute, 0).Ticks;

            var attribute = new DapperMigrationAttribute(year, month, day, hour, minute, author);
            Assert.AreEqual(version, attribute.Version);
        }
    }
}