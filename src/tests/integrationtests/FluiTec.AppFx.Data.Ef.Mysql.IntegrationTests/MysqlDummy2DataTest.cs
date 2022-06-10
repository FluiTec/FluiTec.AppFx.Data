using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Ef.Mysql.IntegrationTests;

/// <summary> (Unit Test Class) a mysql dummy 2 data test.</summary>
[TestClass]
[TestCategory("Integration")]
public class MysqlDummy2DataTest : Dummy2DataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public MysqlDummy2DataTest() : base(new DbProvider())
    {
    }
}