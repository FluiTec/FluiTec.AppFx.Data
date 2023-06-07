using FluiTec.AppFx.Data.Paging;
using FluiTec.AppFx.Data.Tests.Repositories.Fixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.Paging;

[TestClass]
public class PagedResultTest
{
    [TestMethod]
    [DataRow(0, 0)]
    [DataRow(100, 100)]
    public void CanSetPageIndex(int expected, int pageIndex)
    {
        var result = new PagedResult<DummyEntity>(pageIndex, pageIndex + 1, 1, CreateDummies(1));
        Assert.AreEqual(expected, result.PageIndex);
    }

    [TestMethod]
    [DataRow(1, 1)]
    [DataRow(100, 100)]
    public void CanSetPageSize(int expected, int pageSize)
    {
        var result = new PagedResult<DummyEntity>(0, 1, pageSize, CreateDummies(pageSize));
        Assert.AreEqual(expected, result.PageSize);
    }

    [TestMethod]
    [DataRow(false, 0, 1)]
    [DataRow(true, 0, 2)]
    [DataRow(false, 1, 2)]
    public void CanCalculateHasNext(bool expected, int pageIndex, int pageCount)
    {
        var result = new PagedResult<DummyEntity>(pageIndex, pageCount, 1, CreateDummies(1));
        Assert.AreEqual(expected, result.HasNextPage);
    }

    [TestMethod]
    [DataRow(false, 0, 1)]
    [DataRow(true, 1, 2)]
    [DataRow(true, 2, 3)]
    public void CanCalculateHasPrevious(bool expected, int pageIndex, int pageCount)
    {
        var result = new PagedResult<DummyEntity>(pageIndex, pageCount, 1, CreateDummies(1));
        Assert.AreEqual(expected, result.HasPreviousPage);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    [DataRow(-1, 1)]
    [DataRow(1, 1)]
    public void ThrowsOnInvalidPageIndex(int pageIndex, int pageCount)
    {
        new PagedResult<DummyEntity>(pageIndex, pageCount, 1, CreateDummies(1));
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    [DataRow(-1)]
    [DataRow(0)]
    public void ThrowsOnInvalidPageSize(int pageSize)
    {
        new PagedResult<DummyEntity>(0, 1, pageSize, CreateDummies(pageSize));
    }

    private static IEnumerable<DummyEntity> CreateDummies(int count)
    {
        var dummies = new List<DummyEntity>();

        for (var i = 0; i < count; i++) dummies.Add(new DummyEntity { Id = i });

        return dummies;
    }
}