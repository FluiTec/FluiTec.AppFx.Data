using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Sqlite.IntegrationTests;

/// <summary>
///     (Unit Test Class) a sqlite date time dummy data test.
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