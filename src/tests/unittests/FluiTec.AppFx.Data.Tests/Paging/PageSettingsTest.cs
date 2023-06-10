using FluiTec.AppFx.Data.Paging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.Paging;

[TestClass]
public class PageSettingsTest
{
    [TestMethod]
    public void CanUseDefaults()
    {
        var settings = new PageSettings();
        Assert.AreEqual(PageSettings.DefaultPageSize, settings.PageSize);
        Assert.AreEqual(PageSettings.DefaultMaxPageSize, settings.MaxPageSize);
    }

    [TestMethod]
    public void CanSetPageSize()
    {
        const int pageSize = 40;
        var settings = new PageSettings(pageSize);
        Assert.AreEqual(pageSize, settings.PageSize);
    }

    [TestMethod]
    public void CanSetMaxPageSize()
    {
        const int maxpageSize = 200;
        var settings = new PageSettings(20, maxpageSize);
        Assert.AreEqual(maxpageSize, settings.MaxPageSize);
    }
}