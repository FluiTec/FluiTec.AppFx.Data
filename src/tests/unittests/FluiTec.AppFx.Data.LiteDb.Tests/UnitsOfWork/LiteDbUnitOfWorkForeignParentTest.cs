using System.Transactions;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Tests.UnitsOfWork;
using FluiTec.AppFx.Data.Tests.UnitsOfWork.Fixtures;
using FluiTec.AppFx.Data.UnitsOfWork;
using LiteDB;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.LiteDb.Tests.UnitsOfWork;

/// <summary>   (Unit Test Class) a memory unit of work foreign parent test. </summary>
[TestClass]
public class LiteDbUnitOfWorkForeignParentTest : ParentAwareUnitOfWorkTest<TestBaseUnitOfWork, LiteDbUnitOfWork>
{
    /// <summary>   Constructs the given logger. </summary>
    /// <param name="logger">   (Optional) The logger. </param>
    /// <returns>   A TUnitOfWork. </returns>
    protected override TestBaseUnitOfWork Construct(ILogger<IUnitOfWork>? logger = null)
    {
        return new TestBaseUnitOfWork(logger, new TransactionOptions());
    }

    /// <summary>   Construct child. </summary>
    /// <param name="parentAwareUnitOfWork">    The parent aware unit of work. </param>
    /// <param name="logger">                   (Optional) The logger. </param>
    /// <returns>   A TUnitOfWork. </returns>
    protected override LiteDbUnitOfWork ConstructChild(TestBaseUnitOfWork parentAwareUnitOfWork,
        ILogger<IUnitOfWork>? logger = null)
    {
        return new LiteDbUnitOfWork(logger, parentAwareUnitOfWork, new LiteDatabase(":memory:"));
    }
}