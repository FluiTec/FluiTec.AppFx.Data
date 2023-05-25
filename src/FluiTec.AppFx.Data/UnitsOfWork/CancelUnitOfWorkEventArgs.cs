namespace FluiTec.AppFx.Data.UnitsOfWork;

/// <summary>   Additional information for cancel unit of work events. </summary>
public class CancelUnitOfWorkEventArgs : UnitOfWorkEventArgs
{
    /// <summary>   Gets or sets a value indicating whether the cancel. </summary>
    /// <value> True if cancel, false if not. </value>
    public bool Cancel { get; set; }

    /// <summary>   Constructor. </summary>
    /// <param name="unitOfWork">   The unit of work. </param>
    public CancelUnitOfWorkEventArgs(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        Cancel = false;
    }
}