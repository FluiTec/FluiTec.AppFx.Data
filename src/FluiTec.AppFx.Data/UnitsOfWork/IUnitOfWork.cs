﻿using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Repositories;

namespace FluiTec.AppFx.Data.UnitsOfWork
{
    /// <summary>	Interface for a unit of work. </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>Gets the data service.</summary>
        /// <value>The data service.</value>
        // ReSharper disable once UnusedMemberInSuper.Global
        IDataService DataService { get; }

        /// <summary>	Commits this unit of work. </summary>
        // ReSharper disable once UnusedMemberInSuper.Global
        void Commit();

        /// <summary>	Rolls back this unit of work. </summary>
        // ReSharper disable once UnusedMemberInSuper.Global
        void Rollback();

        /// <summary>	Gets the repository. </summary>
        /// <typeparam name="TRepository">	Type of the repository. </typeparam>
        /// <returns>	The repository. </returns>
        // ReSharper disable once UnusedMemberInSuper.Global
        TRepository GetRepository<TRepository>() where TRepository : class, IRepository;
    }
}