using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Repositories;

namespace FluiTec.AppFx.Data.UnitsOfWork
{
    /// <summary>	Interface for a unit of work. </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>Gets the data service.</summary>
        /// <value>The data service.</value>
        IDataService DataService { get; }
        
        /// <summary>	Commits this unit of work. </summary>
        void Commit();

        /// <summary>	Rolls back this unit of work. </summary>
        void Rollback();

        /// <summary>	Gets the repository. </summary>
        /// <typeparam name="TRepository">	Type of the repository. </typeparam>
        /// <returns>	The repository. </returns>
        TRepository GetRepository<TRepository>() where TRepository : class, IRepository;
    }
}