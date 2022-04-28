using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Sqlite.IntegrationTests;

/// <summary>
///     (Unit Test Class) a mssql date time dummy data test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class SqliteDateTimeDummyDataTest : DateTimeDummyDataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public SqliteDateTimeDummyDataTest() : base(new DbProvider())
    {
    }
}