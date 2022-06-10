using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Pgsql.IntegrationTests;

/// <summary> (Unit Test Class) a pgsql dummy 2 data test.</summary>
[TestClass]
[TestCategory("Integration")]
public class PgsqlDummy2DataTest : Dummy2DataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public PgsqlDummy2DataTest() : base(new DbProvider())
    {
    }
}