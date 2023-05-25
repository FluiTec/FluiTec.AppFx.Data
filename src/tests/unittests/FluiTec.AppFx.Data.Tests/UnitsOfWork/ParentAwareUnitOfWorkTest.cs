using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluiTec.AppFx.Data.Tests.UnitsOfWork;

/// <summary>   (Unit Test Class) a parent aware unit of work test. </summary>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
[TestClass]
public abstract class ParentAwareUnitOfWorkTest<TUnitOfWork> : BaseUnitOfWorkTest<TUnitOfWork>
    where TUnitOfWork : ParentAwareUnitOfWork
{
    /// <summary>   (Unit Test Method) can construct child. </summary>
    [TestMethod]
    public void CanConstructChild()
    {
        var parent = Construct();
        var child = ConstructChild(parent);
        Assert.IsNotNull(child);
    }
    /// <summary>   (Unit Test Method) child can not commit by default. </summary>
    [TestMethod]
    public void ChildCanNotCommitByDefault()
    {
        var parent = Construct();
        var child = ConstructChild(parent);

        if (child.ControlledByParent)
            Assert.IsFalse(child.CanCommit);
        else
            Assert.IsTrue(child.CanCommit);
    }
    
    /// <summary>   (Unit Test Method) child is not finished by default. </summary>
    [TestMethod]
    public void ChildIsNotFinishedByDefault()
    {
        var parent = Construct();
        var child = ConstructChild(parent);
        Assert.IsFalse(child.IsFinished);
    }
    
    /// <summary>   (Unit Test Method) child returns null logger. </summary>
    [TestMethod]
    public void ChildReturnsNullLogger()
    {
        var parent = Construct();
        var child = ConstructChild(parent);
        Assert.IsNull(child.Logger);
    }
    
    /// <summary>   (Unit Test Method) child returns logger. </summary>
    [TestMethod]
    public void ChildReturnsLogger()
    {
        var logger = new Mock<ILogger<IUnitOfWork>?>();
        var parent = Construct();
        var child = ConstructChild(parent, logger.Object);
        Assert.IsNotNull(child.Logger);
    }
    
    /// <summary>   (Unit Test Method) child can commit. </summary>
    [TestMethod]
    public void ChildCanCommit()
    {
        var passed = false;
        var parent = Construct();
        var child = ConstructChild(parent);

        if (child.ControlledByParent)
        {
            try
            {
                child.Commit();
            }
            catch (InvalidOperationException)
            {
                parent.Commit();
                passed = child is { CanCommit: false, IsFinished: true };
            }
            Assert.IsTrue(passed);
        }
        else
        {
            child.Commit();
            Assert.IsTrue(child.IsFinished);
            Assert.IsFalse(child.CanCommit);
        }
    }
    
    /// <summary>   (Unit Test Method) child can cancel commit. </summary>
    [TestMethod]
    public void ChildCanCancelCommit()
    {
        var parent = Construct();
        var child = ConstructChild(parent);

        if (child.ControlledByParent)
        {
            child.BeforeCommit += (sender, args) => args.Cancel = true;
            parent.Commit();
            Assert.IsFalse(child.IsFinished);
            Assert.IsFalse(parent.IsFinished);
            Assert.IsTrue(parent.CanCommit);
        }
        else
        {
            child.BeforeCommit += (sender, args) => args.Cancel = true;
            child.Commit();
            Assert.IsFalse(child.IsFinished);
            Assert.IsTrue(child.CanCommit);
        }
    }
    
    /// <summary>   (Unit Test Method) child notifies committed. </summary>
    [TestMethod]
    public void ChildNotifiesCommitted()
    {
        var notified = false;
        var parent = Construct();
        var child = ConstructChild(parent);
        child.Commited += (sender, args) => notified = true;

        if (child.ControlledByParent)
        {
            parent.Commit();
            Assert.IsTrue(child.IsFinished);
            Assert.IsFalse(child.CanCommit);
            Assert.IsTrue(notified);
        }
        else
        {
            child.Commit();
            Assert.IsTrue(child.IsFinished);
            Assert.IsFalse(child.CanCommit);
            Assert.IsTrue(notified);
        }
    }
    
    /// <summary>   (Unit Test Method) child can rollback. </summary>
    [TestMethod]
    public void ChildCanRollback()
    {
        var passed = false;
        var parent = Construct();
        var child = ConstructChild(parent);

        if (child.ControlledByParent)
        {
            try
            {
                child.Rollback();
            }
            catch (InvalidOperationException)
            {
                parent.Rollback();
                passed = child is { CanCommit: false, IsFinished: true };
            }
            Assert.IsTrue(passed);
        }
        else
        {
            child.Rollback();
            Assert.IsTrue(child.IsFinished);
            Assert.IsFalse(child.CanCommit);
        }
    }
    
    /// <summary>   (Unit Test Method) child can cancel rollback. </summary>
    [TestMethod]
    public void ChildCanCancelRollback()
    {
        var parent = Construct();
        var child = ConstructChild(parent);

        if (child.ControlledByParent)
        {
            child.BeforeRollback += (sender, args) => args.Cancel = true;
            parent.Rollback();
            Assert.IsFalse(child.IsFinished);
            Assert.IsFalse(parent.IsFinished);
            Assert.IsTrue(parent.CanCommit);
        }
        else
        {
            child.BeforeRollback += (sender, args) => args.Cancel = true;
            child.Rollback();
            Assert.IsFalse(child.IsFinished);
            Assert.IsTrue(child.CanCommit);
        }
    }

    /// <summary>   (Unit Test Method) child notifies rolled back. </summary>
    [TestMethod]
    public void ChildNotifiesRolledBack()
    {
        var notified = false;
        var parent = Construct();
        var child = ConstructChild(parent);
        child.Rolledback += (sender, args) => notified = true;

        if (child.ControlledByParent)
        {
            parent.Rollback();
            Assert.IsTrue(child.IsFinished);
            Assert.IsFalse(child.CanCommit);
            Assert.IsTrue(notified);
        }
        else
        {
            child.Rollback();
            Assert.IsTrue(child.IsFinished);
            Assert.IsFalse(child.CanCommit);
            Assert.IsTrue(notified);
        }
    }

    /// <summary>   Construct child. </summary>
    /// <param name="parentAwareUnitOfWork">    The parent aware unit of work. </param>
    /// <param name="logger">                   (Optional) The logger. </param>
    /// <returns>   A TUnitOfWork. </returns>
    protected abstract TUnitOfWork ConstructChild(TUnitOfWork parentAwareUnitOfWork, ILogger<IUnitOfWork>? logger = null);
}