using System;
using System.Data;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Data.Dapper.UnitsOfWork
{
    /// <summary>   A dapper unit of work. </summary>
    public class DapperUnitOfWork : UnitOfWork
    {
        #region Fields

        /// <summary>True to owns connection.</summary>
        private readonly bool _ownsConnection = true;

        #endregion

        #region IDisposable

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting
        ///     unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to
        ///     release only unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (Transaction != null)
                Rollback();
        }

        #endregion

        #region Properties

        /// <summary>   Gets or sets the connection. </summary>
        /// <value> The connection. </value>
        public IDbConnection Connection { get; private set; }

        /// <summary>   Gets or sets the transaction. </summary>
        /// <value> The transaction. </value>
        public IDbTransaction Transaction { get; private set; }

        #endregion

        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public DapperUnitOfWork(DapperDataService<DapperUnitOfWork> dataService, ILogger<IUnitOfWork> logger)
            : base(dataService, logger)
        {
            // create and open connection
            Connection = dataService.ConnectionFactory.CreateConnection(dataService.ConnectionString);
            Connection.Open();

            // begin transaction
            Transaction = Connection.BeginTransaction();
        }

        /// <summary>   Constructor. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public DapperUnitOfWork(DapperUnitOfWork parentUnitOfWork, DapperDataService<DapperUnitOfWork> dataService,
            ILogger<IUnitOfWork> logger)
            : base(dataService, logger)
        {
            if (parentUnitOfWork == null) throw new ArgumentNullException(nameof(parentUnitOfWork));
            _ownsConnection = false;
            Connection = parentUnitOfWork.Connection;
            Transaction = parentUnitOfWork.Transaction;
        }

        #endregion

        #region IUnitOfWork

        /// <summary>   Gets or sets the logger factory. </summary>
        /// <summary>   Commits the UnitOfWork. </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is
        ///     invalid.
        /// </exception>
        public override void Commit()
        {
            if (Transaction == null)
                throw new InvalidOperationException(
                    "UnitOfWork can't be committed since it's already finished. (Missing transaction)");
            if (!_ownsConnection) return;

            // clear transaction
            Transaction.Commit();
            Transaction.Dispose();
            Transaction = null;

            // clear connection
            Connection.Close();
            Connection.Dispose();
            Connection = null;
        }

        /// <summary>   Rolls back the UnitOfWork. </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is
        ///     invalid.
        /// </exception>
        public override void Rollback()
        {
            if (Transaction == null)
                throw new InvalidOperationException(
                    "UnitOfWork can't be rolled back since it's already finished. (Missing transaction)");
            if (!_ownsConnection) return;

            // clear transaction
            Transaction.Rollback();
            Transaction.Dispose();
            Transaction = null;

            // clear connection
            Connection.Close();
            Connection.Dispose();
            Connection = null;
        }

        #endregion
    }
}