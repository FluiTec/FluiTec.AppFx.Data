using FluiTec.AppFx.Data.NMemory.Tests.Providers.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NMemory;

namespace FluiTec.AppFx.Data.NMemory.Tests.Providers;

[TestClass]
public class ConfigurableNMemoryDataProviderTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnMissingDataService()
    {
        new ConfigurableNMemoryTestDataProvider(() => new Database(), null!);
    }

    [TestMethod]
    public void SetsDataService()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider = new ConfigurableNMemoryTestDataProvider(() => new Database(), dataServiceMock.Object);
        Assert.IsNotNull(provider.DataService);
    }

    [TestMethod]
    public void SetsDatabase()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider = new ConfigurableNMemoryTestDataProvider(() => new Database(), dataServiceMock.Object);
        Assert.IsNotNull(provider.Database);
    }

    [TestMethod]
    public void InitializesNameStrategy()
    {
        var dataServiceMock = new Mock<ITestDataService>();
        var provider = new ConfigurableNMemoryTestDataProvider(() => new Database(), dataServiceMock.Object);
        Assert.IsNotNull(provider.NameStrategy);
    }
}