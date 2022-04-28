using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Pgsql.IntegrationTests;

/// <summary>
///     (Unit Test Class) a mssql entity data test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class PgsqlDummyDataTest : DummyDataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public PgsqlDummyDataTest() : base(new DbProvider())
    {
    }
}