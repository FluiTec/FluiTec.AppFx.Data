using System;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.UnitsOfWork
{
    /// <summary>	Interface for a unit of work. </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        ILogger<IUnitOfWork> Logger { get; }

        /// <summary>	Commits this unit of work. </summary>
        void Commit();

        /// <summary>	Rolls back this unit of work. </summary>
        void Rollback();
    }
}