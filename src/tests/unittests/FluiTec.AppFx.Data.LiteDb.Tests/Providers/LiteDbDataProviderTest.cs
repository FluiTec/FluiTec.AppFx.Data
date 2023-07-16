using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.LiteDb.Tests.Providers;

[TestClass]
public class LiteDbDataProviderTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataService()
    {
        new NMemoryTestDataProvider(null!, new DataOptions());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingOptions()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        new NMemoryTestDataProvider(dataServiceMock.Object, (DataOptions)null!);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingOptionsMonitor()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        new NMemoryTestDataProvider(dataServiceMock.Object, (IOptionsMonitor<DataOptions>)null!);
    }

    [TestMethod]
    public void SetsDataService()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider = new NMemoryTestDataProvider(dataServiceMock.Object, new DataOptions());
        Assert.IsNotNull(provider.DataService);
    }

    [TestMethod]
    public void SetsDatabase()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider = new NMemoryTestDataProvider(dataServiceMock.Object, new DataOptions());
        Assert.IsNotNull(provider.Database);
    }

    [TestMethod]
    public void InitializesNameStrategy()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider = new NMemoryTestDataProvider(dataServiceMock.Object, new DataOptions());
        Assert.IsNotNull(provider.NameStrategy);
    }

    [TestMethod]
    public void InitializesPageSettings()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider = new NMemoryTestDataProvider(dataServiceMock.Object, new DataOptions());
        Assert.IsNotNull(provider.PageSettings);
    }
}