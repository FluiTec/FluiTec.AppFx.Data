using System;
using FluiTec.AppFx.Data.DataServices;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.UnitsOfWork
{
    /// <summary></summary>
    /// <seealso cref="FluiTec.AppFx.Data.UnitsOfWork.IUnitOfWork" />
    public abstract class UnitOfWork : IUnitOfWork
    {
        private IDataService DataService { get; }

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        public ILogger<IUnitOfWork> Logger { get; }

        protected UnitOfWork(IDataService dataService, ILogger<IUnitOfWork> logger)
        {
            DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            Logger = logger; // we accept null here
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged
        ///     resources.
        /// </summary>
        public abstract void Dispose();

        /// <summary>	Gets or sets the logger factory. </summary>
        /// <value>	The logger factory. </value>
        /// <summary>   Commits the UnitOfWork. </summary>
        public abstract void Commit();

        /// <summary>   Rolls back the UnitOfWork. </summary>
        public abstract void Rollback();
    }
}