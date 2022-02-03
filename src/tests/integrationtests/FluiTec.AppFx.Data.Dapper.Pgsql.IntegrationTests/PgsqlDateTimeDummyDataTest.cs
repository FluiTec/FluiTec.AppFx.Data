using FluiTec.AppFx.Data.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Pgsql.IntegrationTests;

/// <summary>
///     (Unit Test Class) a pgsql date time dummy data test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class PgsqlDateTimeDummyDataTest : DateTimeDummyDataTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public PgsqlDateTimeDummyDataTest() : base(new DbProvider())
    {
    }
}