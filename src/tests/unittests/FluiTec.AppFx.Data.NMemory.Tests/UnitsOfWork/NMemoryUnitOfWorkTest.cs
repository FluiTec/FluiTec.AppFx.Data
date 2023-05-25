using System.Transactions;
using FluiTec.AppFx.Data.Tests.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.NMemory.Tests.UnitsOfWork;

/// <summary>   (Unit Test Class) a memory unit of work test. </summary>
[TestClass]
public class NMemoryUnitOfWorkTest : ParentAwareUnitOfWorkTest<NMemoryUnitOfWork>
{
    /// <summary>   Constructs the given logger. </summary>
    /// <param name="logger">   (Optional) The logger. </param>
    /// <returns>   A TUnitOfWork. </returns>
    protected override NMemoryUnitOfWork Construct(ILogger<IUnitOfWork>? logger = null)
    {
        return new(logger, new TransactionOptions());
    }

    /// <summary>   Construct child. </summary>
    /// <param name="parentAwareUnitOfWork">    The parent aware unit of work. </param>
    /// <param name="logger">                   (Optional) The logger. </param>
    /// <returns>   A TUnitOfWork. </returns>
    protected override NMemoryUnitOfWork ConstructChild(NMemoryUnitOfWork parentAwareUnitOfWork,
        ILogger<IUnitOfWork>? logger = null)
    {
        return new(logger, parentAwareUnitOfWork);
    }
}