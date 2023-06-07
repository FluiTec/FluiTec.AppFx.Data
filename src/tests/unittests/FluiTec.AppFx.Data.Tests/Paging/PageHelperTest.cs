using FluiTec.AppFx.Data.Paging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.Paging;

[TestClass]
public class PageHelperTest
{
    [TestMethod]
    [DataRow(0, -1)]
    [DataRow(0, 0)]
    [DataRow(100, 100)]
    public void CanFixIndex(int expected, int input)
    {
        Assert.AreEqual(expected, PageHelper.FixPageIndex(input));
    }

    [TestMethod]
    [DataRow(1, -1)]
    [DataRow(1, 0)]
    [DataRow(100, 100)]
    [DataRow(100, 200)]
    public void CanFixSize(int expected, int input)
    {
        Assert.AreEqual(expected, PageHelper.FixPageSize(input, new PageSettings()));
    }
}