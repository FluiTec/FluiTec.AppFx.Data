using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Mysql.IntegrationTests;

/// <summary>
///     (Unit Test Class) a mysql date time dummy data test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class MysqlDateTimeDummyDataTest : DateTimeDummyDataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public MysqlDateTimeDummyDataTest() : base(new DbProvider())
    {
    }
}