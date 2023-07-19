using FluiTec.AppFx.Data.LiteDb.Tests.Providers.Fixtures;
using FluiTec.AppFx.Data.Options;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LiteDB;

// ReSharper disable ObjectCreationAsStatement

namespace FluiTec.AppFx.Data.LiteDb.Tests.Providers;

[TestClass]
public class ConfigurableLiteDbDataProviderTest
{
    private const string Constr = ":memory:";

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataService()
    {
        new ConfigurableLiteDbTestDataProvider(() => new LiteDatabase(Constr), null!, new DataOptions());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingOptions()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        new ConfigurableLiteDbTestDataProvider(() => new LiteDatabase(Constr), dataServiceMock.Object, (DataOptions)null!);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingOptionsMonitor()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        new ConfigurableLiteDbTestDataProvider(() => new LiteDatabase(Constr), dataServiceMock.Object,
            (IOptionsMonitor<DataOptions>)null!);
    }

    [TestMethod]
    public void SetsDataService()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider =
            new ConfigurableLiteDbTestDataProvider(() => new LiteDatabase(Constr), dataServiceMock.Object, new DataOptions());
        Assert.IsNotNull(provider.DataService);
    }

    [TestMethod]
    public void SetsDatabase()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider =
            new ConfigurableLiteDbTestDataProvider(() => new LiteDatabase(Constr), dataServiceMock.Object, new DataOptions());
        Assert.IsNotNull(provider.Database);
    }

    [TestMethod]
    public void InitializesNameStrategy()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider =
            new ConfigurableLiteDbTestDataProvider(() => new LiteDatabase(Constr), dataServiceMock.Object, new DataOptions());
        Assert.IsNotNull(provider.NameStrategy);
    }

    [TestMethod]
    public void InitializesPageSettings()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider =
            new ConfigurableLiteDbTestDataProvider(() => new LiteDatabase(Constr), dataServiceMock.Object, new DataOptions());
        Assert.IsNotNull(provider.PageSettings);
    }
}