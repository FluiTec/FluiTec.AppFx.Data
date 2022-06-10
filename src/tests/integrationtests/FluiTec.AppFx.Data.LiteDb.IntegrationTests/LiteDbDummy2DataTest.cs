using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.LiteDb.IntegrationTests;

/// <summary> (Unit Test Class) a lite database dummy 2 data test.</summary>
[TestClass]
[TestCategory("Integration")]
public class LiteDbDummy2DataTest : Dummy2DataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public LiteDbDummy2DataTest() : base(new DbProvider())
    {
    }
}