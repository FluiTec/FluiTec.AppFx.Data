using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.NMemory.IntegrationTests;

/// <summary>
///     (Unit Test Class) a memory date time dummy data test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class NMemoryDateTimeDummyDataTest : DateTimeDummyDataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public NMemoryDateTimeDummyDataTest() : base(new DbProvider())
    {
    }
}