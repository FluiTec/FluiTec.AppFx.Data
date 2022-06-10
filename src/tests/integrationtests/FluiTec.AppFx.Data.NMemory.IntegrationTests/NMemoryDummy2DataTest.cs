using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.NMemory.IntegrationTests;

/// <summary> (Unit Test Class) a memory dummy 2 data test.</summary>
[TestClass]
[TestCategory("Integration")]
public class NMemoryDummy2DataTest : Dummy2DataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public NMemoryDummy2DataTest() : base(new DbProvider())
    {
    }
}