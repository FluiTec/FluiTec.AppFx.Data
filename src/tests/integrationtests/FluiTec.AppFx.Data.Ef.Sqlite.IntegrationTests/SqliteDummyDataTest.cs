using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Sqlite.IntegrationTests;

/// <summary>
///     (Unit Test Class) a mssql entity data test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class SqliteDummyDataTest : DummyDataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public SqliteDummyDataTest() : base(new DbProvider())
    {
    }
}