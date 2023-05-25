using System;

namespace FluiTec.AppFx.Data.UnitsOfWork;

/// <summary>   Additional information for unit of work events. </summary>
public class UnitOfWorkEventArgs : EventArgs
{
    /// <summary>   Gets the unit of work. </summary>
    /// <value> The unit of work. </value>
    public IUnitOfWork UnitOfWork { get; }

    /// <summary>   Constructor. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    public UnitOfWorkEventArgs(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
}