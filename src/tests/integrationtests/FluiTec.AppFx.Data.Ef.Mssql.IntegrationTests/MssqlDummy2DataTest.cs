using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Mssql.IntegrationTests;

/// <summary> A mssql dummy 2 data test.</summary>
[TestClass]
[TestCategory("Integration")]
public class MssqlDummy2DataTest : Dummy2DataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public MssqlDummy2DataTest() : base(new DbProvider())
    {
    }
}