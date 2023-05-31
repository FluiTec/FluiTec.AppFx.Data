using System.Transactions;
using FluiTec.AppFx.Data.Tests.UnitsOfWork.Fixtures;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.UnitsOfWork;

/// <summary>   A test parent aware unit of work test. </summary>
[TestClass]
public class
    TestParentAwareUnitOfWorkTest : ParentAwareUnitOfWorkTest<TestParentAwareUnitOfWork, TestParentAwareUnitOfWork>
{
    /// <summary>   Constructs the given logger. </summary>
    /// <param name="logger">   (Optional) The logger. </param>
    /// <returns>   A TUnitOfWork. </returns>
    protected override TestParentAwareUnitOfWork Construct(ILogger<IUnitOfWork>? logger = null)
    {
        return new(logger, new TransactionOptions());
    }

    /// <summary>   Construct child. </summary>
    /// <param name="parentAwareUnitOfWork">    The parent aware unit of work. </param>
    /// <param name="logger">                   (Optional) The logger. </param>
    /// <returns>   A TestParentAwareUnitOfWork. </returns>
    protected override TestParentAwareUnitOfWork ConstructChild(TestParentAwareUnitOfWork parentAwareUnitOfWork,
        ILogger<IUnitOfWork>? logger = null)
    {
        return new(logger, parentAwareUnitOfWork);
    }
}