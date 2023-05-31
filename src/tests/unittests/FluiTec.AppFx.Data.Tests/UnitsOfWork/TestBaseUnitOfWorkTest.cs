using System.Transactions;
using FluiTec.AppFx.Data.Tests.UnitsOfWork.Fixtures;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Data.Tests.UnitsOfWork;

/// <summary>   (Unit Test Class) a test base unit of work test. </summary>
[TestClass]
public class TestBaseUnitOfWorkTest : BaseUnitOfWorkTest<TestBaseUnitOfWork>
{
    /// <summary>   Constructs the given logger. </summary>
    /// <param name="logger">   (Optional) The logger. </param>
    /// <returns>   A TUnitOfWork. </returns>
    protected override TestBaseUnitOfWork Construct(ILogger<IUnitOfWork>? logger = null)
    {
        return new(logger, new TransactionOptions());
    }
}