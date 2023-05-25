using System.Transactions;
using FluiTec.AppFx.Data.UnitsOfWork;

namespace FluiTec.AppFx.Data.DataProviders;

/// <summary>   Interface for data provider. </summary>
/// <typeparam name="TUnitOfWork">  Type of the unit of work. </typeparam>
public interface IDataProvider<out TUnitOfWork> 
    where TUnitOfWork : IUnitOfWork
{
    /// <summary>   Begins unit of work. </summary>
    /// <returns>   A TUnitOfWork. </returns>
    TUnitOfWork BeginUnitOfWork();

    /// <summary>   Begins unit of work. </summary>
    /// <param name="transactionOptions">   Options for controlling the transaction. </param>
    /// <returns>   A TUnitOfWork. </returns>
    TUnitOfWork BeginUnitOfWork(TransactionOptions transactionOptions);
}