using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

// ReSharper disable ObjectCreationAsStatement

namespace FluiTec.AppFx.Data.Tests.UnitsOfWork;

[TestClass]
public class UnitOfWorkEventArgsTest
{
    [TestMethod]
    public void CanConstruct()
    {
        var uowMock = new Mock<IUnitOfWork>();
        var args = new UnitOfWorkEventArgs(uowMock.Object);
        Assert.IsNotNull(args);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ThrowsOnNullArgument()
    {
        new UnitOfWorkEventArgs(null!);
    }

    [TestMethod]
    public void CanSaveUnitOfWorkArgument()
    {
        var uowMock = new Mock<IUnitOfWork>();
        var args = new UnitOfWorkEventArgs(uowMock.Object);
        Assert.IsNotNull(args.UnitOfWork);
    }
}