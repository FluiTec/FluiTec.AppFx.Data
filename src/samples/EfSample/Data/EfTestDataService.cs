using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Ef;
using FluiTec.AppFx.Data.Ef.DataServices;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EfSample.Data;

public class EfTestDataService : BaseEfDataService<EfTestUnitOfWork>, ITestDataService
{
    public EfTestDataService(ISqlServiceOptions options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
    {
    }

    public EfTestDataService(IOptionsMonitor<ISqlServiceOptions> options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
    {
    }

    /// <summary>
    /// Gets the name.
    /// </summary>
    ///
    /// <value>
    /// The name.
    /// </value>
    public override string Name => nameof(EfTestDataService);

    /// <summary>
    /// Gets a value indicating whether the supports migration.
    /// </summary>
    ///
    /// <value>
    /// True if supports migration, false if not.
    /// </value>
    public override bool SupportsMigration => false;

    /// <summary>
    /// Gets the migrator.
    /// </summary>
    ///
    /// <returns>
    /// The migrator.
    /// </returns>
    public override IDataMigrator GetMigrator()
    {
        throw new NotImplementedException();
    }

    public override EfTestUnitOfWork BeginUnitOfWork()
    {
        return new EfTestUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
    }

    public override EfTestUnitOfWork BeginUnitOfWork(IUnitOfWork other)
    {
        throw new NotImplementedException();
    }

    public override SqlType SqlType { get; }

    public override DynamicDbContext GetContext()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Begins unit of work.
    /// </summary>
    /// <returns>
    /// A TUnitOfWork.
    /// </returns>
    ITestUnitOfWork IDataService<ITestUnitOfWork>.BeginUnitOfWork(IUnitOfWork other)
    {
        return BeginUnitOfWork(other);
    }

    /// <summary>
    /// Begins unit of work.
    /// </summary>
    /// <returns>
    /// A TUnitOfWork.
    /// </returns>
    ITestUnitOfWork IDataService<ITestUnitOfWork>.BeginUnitOfWork()
    {
        return BeginUnitOfWork();
    }
}