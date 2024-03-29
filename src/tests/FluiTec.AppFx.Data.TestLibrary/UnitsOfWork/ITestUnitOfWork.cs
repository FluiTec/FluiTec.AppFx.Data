﻿using FluiTec.AppFx.Data.TestLibrary.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.TestLibrary.UnitsOfWork
{
    /// <summary>   Interface for test unit of work. </summary>
    public interface ITestUnitOfWork : IUnitOfWork
    {
        /// <summary>   Gets the dummy repository. </summary>
        /// <value> The dummy repository. </value>
        // ReSharper disable once UnusedMember.Global
        IDummyRepository DummyRepository { get; }

        /// <summary>
        /// Gets the dummy 2 repository.
        /// </summary>
        ///
        /// <value>
        /// The dummy 2 repository.
        /// </value>
        IDummy2Repository Dummy2Repository { get; }

        /// <summary>
        ///     Gets the date time dummy repository.
        /// </summary>
        /// <value>
        ///     The date time dummy repository.
        /// </value>
        // ReSharper disable once UnusedMember.Global
        IDateTimeDummyRepository DateTimeDummyRepository { get; }
    }
}