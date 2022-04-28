using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Mssql.IntegrationTests;

/// <summary>
///     (Unit Test Class) a mssql entity data test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class MssqlDummyDataTest : DummyDataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public MssqlDummyDataTest() : base(new DbProvider())
    {
    }
}