using EfSample.Data.Context;
using EfSample.Data.Schema;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Ef;
using FluiTec.AppFx.Data.Ef.DataServices;
using FluiTec.AppFx.Data.Ef.UnitsOfWork;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EfSample.Data;

/// <summary>
/// A service for accessing ef test data information.
/// </summary>
public class EfTestDataService : EfDataService<EfTestUnitOfWork>, ITestDataService
{
    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="options">          Options for controlling the operation. </param>
    /// <param name="loggerFactory">    The logger factory. </param>
    public EfTestDataService(ISqlServiceOptions options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="options">          Options for controlling the operation. </param>
    /// <param name="loggerFactory">    The logger factory. </param>
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
    /// Gets the schema.
    /// </summary>
    ///
    /// <value>
    /// The schema.
    /// </value>
    public override string Schema => SchemaGlobals.Schema;

    /// <summary>
    /// Begins unit of work.
    /// </summary>
    ///
    /// <returns>
    /// An IUnitOfWork.
    /// </returns>
    public override EfTestUnitOfWork BeginUnitOfWork()
    {
        return new EfTestUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
    }

    /// <summary>
    /// Begins unit of work.
    /// </summary>
    ///
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <exception cref="ArgumentException">        Thrown when one or more arguments have
    ///                                             unsupported or illegal values. </exception>
    ///
    /// <param name="other">    The other. </param>
    ///
    /// <returns>
    /// An IUnitOfWork.
    /// </returns>
    public override EfTestUnitOfWork BeginUnitOfWork(IUnitOfWork other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));
        if (other is not EfUnitOfWork work)
            throw new ArgumentException(
                $"Incompatible implementation of UnitOfWork. Must be of type {nameof(EfUnitOfWork)}!");
        return new EfTestUnitOfWork(work, this, LoggerFactory?.CreateLogger<IUnitOfWork>());
    }
    
    /// <summary>
    /// Gets the context.
    /// </summary>
    ///
    /// <returns>
    /// The context.
    /// </returns>
    public override IDynamicDbContext GetContext() => new TestDbContext(SqlType, ConnectionString);

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