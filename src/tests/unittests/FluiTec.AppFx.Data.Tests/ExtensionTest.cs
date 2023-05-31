using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests;

[TestClass]
public class ExtensionTest
{
    [TestMethod]
    public void AddsTypedDataService()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        services.AddDataService<ITestDataService, TestDataService>();

        var sp = services.BuildServiceProvider();

        var service = sp.GetService<TestDataService>();
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void AddsCommonInterfacedDataService()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        services.AddDataService<ITestDataService, TestDataService>();

        var sp = services.BuildServiceProvider();

        var service = sp.GetService<IDataService>();
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void AddsSpecificInterfacedDataService()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ILoggerFactory, NullLoggerFactory>();
        services.AddDataService<ITestDataService, TestDataService>();

        var sp = services.BuildServiceProvider();

        var service = sp.GetService<IDataService>();
        Assert.IsNotNull(service);
    }
}