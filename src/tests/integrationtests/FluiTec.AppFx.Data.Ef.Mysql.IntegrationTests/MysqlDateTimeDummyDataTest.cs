using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Mysql.IntegrationTests;

/// <summary>
///     (Unit Test Class) a mssql date time dummy data test.
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