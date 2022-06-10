using FluiTec.AppFx.Data.TestLibrary;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using FluiTec.AppFx.Data.TestLibrary.DataServices;
using FluiTec.AppFx.Data.TestLibrary.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Dapper.Sqlite.IntegrationTests;

/// <summary> (Unit Test Class) a pgsql dummy 2 data test.</summary>
[TestClass]
[TestCategory("Integration")]
public class SqliteDummy2DataTest : Dummy2DataTest
{
    /// <summary> Constructor.</summary>
    ///
    /// <param name="dataServiceProvider"> The data service provider. </param>
    public SqliteDummy2DataTest(DataServiceProvider<ITestDataService, ITestUnitOfWork> dataServiceProvider) : base(dataServiceProvider)
    {
    }
}