using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Sqlite.IntegrationTests;

/// <summary> (Unit Test Class) a sqlite dummy 2 data test.</summary>
[TestClass]
[TestCategory("Integration")]
public class SqliteDummy2DataTest : Dummy2DataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public SqliteDummy2DataTest() : base(new DbProvider())
    {
    }
}