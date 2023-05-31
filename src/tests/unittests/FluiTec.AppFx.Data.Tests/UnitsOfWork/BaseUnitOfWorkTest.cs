using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.Tests.UnitsOfWork;

/// <summary>   (Unit Test Class) a base unit of work test. </summary>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
[TestClass]
public abstract class BaseUnitOfWorkTest<TUnitOfWork>
    where TUnitOfWork : BaseUnitOfWork
{
    /// <summary>   (Unit Test Method) can construct. </summary>
    [TestMethod]
    public void CanConstruct()
    {
        var uow = Construct();
        Assert.IsNotNull(uow);
    }

    /// <summary>   (Unit Test Method) can commit by default. </summary>
    [TestMethod]
    public void CanCommitByDefault()
    {
        var uow = Construct();
        Assert.IsTrue(uow.CanCommit);
    }

    /// <summary>   (Unit Test Method) is not finished by default. </summary>
    [TestMethod]
    public void IsNotFinishedByDefault()
    {
        var uow = Construct();
        Assert.IsFalse(uow.IsFinished);
    }

    /// <summary>   (Unit Test Method) returns null logger. </summary>
    [TestMethod]
    public void ReturnsNullLogger()
    {
        var uow = Construct();
        Assert.IsNull(uow.Logger);
    }

    /// <summary>   (Unit Test Method) returns logger. </summary>
    [TestMethod]
    public void ReturnsLogger()
    {
        var logger = new Mock<ILogger<IUnitOfWork>?>();
        var uow = Construct(logger.Object);
        Assert.IsNotNull(uow.Logger);
    }

    /// <summary>   (Unit Test Method) can commit. </summary>
    [TestMethod]
    public void CanCommit()
    {
        var uow = Construct();
        uow.Commit();
        Assert.IsTrue(uow.IsFinished);
        Assert.IsFalse(uow.CanCommit);
    }

    /// <summary>   (Unit Test Method) can cancel commit. </summary>
    [TestMethod]
    public void CanCancelCommit()
    {
        var uow = Construct();
        uow.BeforeCommit += (sender, args) => args.Cancel = true;
        uow.Commit();
        Assert.IsFalse(uow.IsFinished);
        Assert.IsTrue(uow.CanCommit);
    }

    /// <summary>   (Unit Test Method) notifies committed. </summary>
    [TestMethod]
    public void NotifiesCommitted()
    {
        var notified = false;
        var uow = Construct();
        uow.Commited += (sender, args) => notified = true;
        uow.Commit();
        Assert.IsTrue(uow.IsFinished);
        Assert.IsFalse(uow.CanCommit);
        Assert.IsTrue(notified);
    }

    /// <summary>   (Unit Test Method) can rollback. </summary>
    [TestMethod]
    public void CanRollback()
    {
        var uow = Construct();
        uow.Rollback();
        Assert.IsTrue(uow.IsFinished);
        Assert.IsFalse(uow.CanCommit);
    }

    /// <summary>   (Unit Test Method) can cancel rollback. </summary>
    [TestMethod]
    public void CanCancelRollback()
    {
        var uow = Construct();
        uow.BeforeRollback += (sender, args) => args.Cancel = true;
        uow.Rollback();
        Assert.IsFalse(uow.IsFinished);
        Assert.IsTrue(uow.CanCommit);
    }

    /// <summary>   (Unit Test Method) notifies rolled back. </summary>
    [TestMethod]
    public void NotifiesRolledBack()
    {
        var notified = false;
        var uow = Construct();
        uow.Rolledback += (sender, args) => notified = true;
        uow.Rollback();
        Assert.IsTrue(uow.IsFinished);
        Assert.IsFalse(uow.CanCommit);
        Assert.IsTrue(notified);
    }

    [TestMethod]
    public void RollsBackOnDispose()
    {
        var notified = false;
        var uow = Construct();
        uow.Rolledback += (sender, args) => notified = true;
        uow.Dispose();
        Assert.IsTrue(uow.IsFinished);
        Assert.IsFalse(uow.CanCommit);
        Assert.IsTrue(notified);
    }

    /// <summary>   Constructs the given logger. </summary>
    /// <param name="logger">   (Optional) The logger. </param>
    /// <returns>   A TUnitOfWork. </returns>
    protected abstract TUnitOfWork Construct(ILogger<IUnitOfWork>? logger = null);
}